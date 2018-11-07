namespace BLL.Factory.Implementation.Excel.Books
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Interfaces;
    using DAL.Interfaces;
    using DTO.Entities;
    using OfficeOpenXml;

    internal class BookDtoExcelExtractor : IExtractor
    {
        public event EventHandler<EventArgs> ImportExtracted;

        private readonly IUnitOfWork _unitOfWork;

        public BookDtoExcelExtractor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void Extract()
        {
            M();
        }

        private void ExtractImportData();

        private void M()
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
        }

        private T GetCellValue<T>(ExcelWorksheet worksheet, int row, int column)
        {
            var cellType = typeof(T);
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellType.IsGenericType) // dto collections
            {
                var parsedValues = cellValue.ToString().Split(',').ToList();
                parsedValues.ForEach(str => str.Trim());
                var collectionType = cellType.GetGenericArguments().SingleOrDefault();

                if (collectionType == typeof(AuthorDto))
                {
                    var authors = new List<AuthorDto>();
                    // get/create authors
                }
                else if (collectionType == typeof(GenreDto))
                {
                    var genres = new List<GenreDto>();
                    // get/create genres
                }

                return Activator.CreateInstance<T>(); // never really gets here
            }
            else if (cellType == typeof(Dto)) // other dto properies
            {
                if (cellType == typeof(LibraryDto))
                {
                    var libraryRepository = _unitOfWork.GetRepository<LibraryDto>();
                    var library = libraryRepository.FindBy(lib => lib.Name == cellValue.ToString());

                    if (library != null)
                    {
                        return (T)Convert.ChangeType(library, cellType);
                    }

                    libraryRepository.Insert(new LibraryDto
                    {
                        Name = cellValue.ToString()
                    });
                    _unitOfWork.Save();

                    var newLibrary = libraryRepository.FindBy(lib => lib.Name == cellValue.ToString());
                    return (T)Convert.ChangeType(newLibrary, cellType);
                }
                return Activator.CreateInstance<T>(); // never really gets here
            }
            else // primitive dto properies
            {
                return (T)Convert.ChangeType(cellValue, cellType);
            }
        }
    }
}