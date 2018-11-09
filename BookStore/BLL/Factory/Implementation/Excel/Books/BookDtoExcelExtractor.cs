namespace BLL.Factory.Implementation.Excel.Books
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Interfaces;
    using DAL.Interfaces;
    using DTO.Entities;
    using OfficeOpenXml;
    using BLL.Models;

    internal class BookDtoExcelExtractor : IExtractor
    {
        public event ExtractHandler<ExtractionEventArgs> ExtractionPassed;

        private readonly IUnitOfWork _unitOfWork;

        public BookDtoExcelExtractor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Extract(object sender, ValidationEventArgs argsIn)
        {
            var newData = new List<Dto>(argsIn.EntriesAmount);

            using (var package = new ExcelPackage(argsIn.SourceStream))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    var newItem = new BookDto
                    {
                        Isbn = GetCellValue<string>(worksheet, row, argsIn.SourceMap["Isbn"]),
                        Pages = GetCellValue<int>(worksheet, row, argsIn.SourceMap["Pages"]),
                        LimitedEdition = GetCellValue<bool>(worksheet, row, argsIn.SourceMap["LimitedEdition"]),
                        WrittenIn = GetCellValue<DateTime>(worksheet, row, argsIn.SourceMap["WrittenIn"]),
                        Library = GetCellValue<LibraryDto>(worksheet, row, argsIn.SourceMap["Library"]),
                        Authors = GetCellValue<ICollection<AuthorDto>>(worksheet, row, argsIn.SourceMap["Authors"]),
                        Genres = GetCellValue<ICollection<GenreDto>>(worksheet, row, argsIn.SourceMap["Genres"])
                    };
                    newItem.LibraryId = newItem.Library.Id;
                    newData.Add(newItem);
                }
            }

            var argsOut = new ExtractionEventArgs
            {
                ExtractedData = newData
            };
            ExtractionPassed?.Invoke(this, argsOut);
        }

        private T GetCellValue<T>(ExcelWorksheet worksheet, int row, int column)
        {
            var cellType = typeof(T);
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellType.IsGenericType) // dto collections as properies
            {
                return GetCollection<T>(cellValue, cellType);
            }
            else if (cellType == typeof(Dto)) // another dto as property
            {
                return GetDtoProperty<T>(cellValue, cellType);
            }
            else // primitive type properies
            {
                return (T)Convert.ChangeType(cellValue, cellType);
            }
        }

        private T GetCollection<T>(object cellValue, Type cellType)
        {
            var collectionType = cellType.GetGenericArguments().SingleOrDefault();
            var parsedValues = cellValue.ToString().Split(',').ToList();
            parsedValues.ForEach(str => str.Trim());
            List<Dto> collection = new List<Dto>(parsedValues.Count);

            if (collectionType == typeof(AuthorDto))
            {
                FillCollection<AuthorDto>(collection, parsedValues);
            }
            else if (collectionType == typeof(GenreDto))
            {
                FillCollection<GenreDto>(collection, parsedValues);
            }

            return (T)Convert.ChangeType(collection, cellType);
        }

        private void FillCollection<T>(List<Dto> collection, List<string> values)
            where T : Dto
        {
            var repository = _unitOfWork.GetRepository<T>();

            values.ForEach(importedValue =>
            {
                var possibleEntry = repository.FindBy(dto => dto.Name == importedValue).SingleOrDefault();
                if (possibleEntry != null)
                    collection.Add(possibleEntry);
            });
        }

        private T GetDtoProperty<T>(object cellValue, Type cellType)
        {
            T dtoProperty = default(T);

            if (cellType == typeof(LibraryDto))
            {
                var dtoPropContent = FillDtoProperty<LibraryDto>(cellValue.ToString(), cellType);
                dtoProperty = (T)Convert.ChangeType(dtoPropContent, cellType);
            }
            return dtoProperty;
        }

        private T FillDtoProperty<T>(string dtoName, Type cellType)
            where T : Dto
        {
            var repository = _unitOfWork.GetRepository<T>();
            var possibleEntry = repository.FindBy(dto => dto.Name == dtoName).SingleOrDefault();

            throw new NotImplementedException();
        }
    }
}