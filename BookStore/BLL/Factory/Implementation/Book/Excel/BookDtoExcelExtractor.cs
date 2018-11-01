namespace BLL.Factory.Implementation.Book.Excel
{
    using System;
    using System.Web;
    using System.Linq;
    using System.Collections.Generic;
    using Interfaces;
    using DAL.Interfaces;
    using DTO.Entities;
    using OfficeOpenXml;

    public class BookDtoExcelExtractor : IExtractor
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookDtoExcelExtractor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Dto> Extract(HttpPostedFile source, object sourceMap)
        {
            if (!(sourceMap is Dictionary<string, int> castedMap))
                throw new InvalidCastException("Cant cast sourceMap of imported excel.");

            var newData = new List<Dto>();
            using (var package = new ExcelPackage(source.InputStream))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    var newItem = new BookDto
                    {
                        Isbn = GetCellValue<string>(worksheet, row, castedMap["Isbn"]),
                        Pages = GetCellValue<int>(worksheet, row, castedMap["Pages"]),
                        LimitedEdition = GetCellValue<bool>(worksheet, row, castedMap["LimitedEdition"]),
                        WrittenIn = GetCellValue<DateTime>(worksheet, row, castedMap["WrittenIn"]),
                        Library = GetCellValue<LibraryDto>(worksheet, row, castedMap["Library"]),
                        Authors = GetCellValue<ICollection<AuthorDto>>(worksheet, row, castedMap["Authors"]),
                        Genres = GetCellValue<ICollection<GenreDto>>(worksheet, row, castedMap["Genres"])
                    };
                    newItem.LibraryId = newItem.Library.Id;
                    newData.Add(newItem);
                }
            }
            return newData;
        }

        private T GetCellValue<T>(ExcelWorksheet worksheet, int row, int column)
        {
            var cellType = typeof(T);
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellType.IsGenericType)
            {
                var parsedValues = cellValue.ToString().Split(',').ToList();
                parsedValues.ForEach(str => str.Trim());
                //

                return Activator.CreateInstance<T>();
            }
            else if (cellType == typeof(Dto))
            {
                if (cellType == typeof(LibraryDto))
                {
                    //return _unitOfWork.GetRepository<BookDto>().FindBy(book => book.Name == cellValue.ToString());
                }
                return Activator.CreateInstance<T>();
            }
            else
            {
                return (T)Convert.ChangeType(cellValue, cellType);
            }
        }
    }
}