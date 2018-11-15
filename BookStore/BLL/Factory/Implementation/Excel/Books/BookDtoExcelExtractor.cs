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
                        Isbn = GetSimple<string>(worksheet.Cells[row, argsIn.SourceMap["Isbn"]].Value),
                        Pages = GetSimple<int>(worksheet.Cells[row, argsIn.SourceMap["Pages"]].Value),
                        LimitedEdition = GetSimple<bool>(worksheet.Cells[row, argsIn.SourceMap["LimitedEdition"]].Value),
                        WrittenIn = GetSimple<DateTime>(worksheet.Cells[row, argsIn.SourceMap["WrittenIn"]].Value),
                        Library = GetDto<LibraryDto>(worksheet.Cells[row, argsIn.SourceMap["Library"]].Value),
                        Authors = GetCollection<AuthorDto>(worksheet.Cells[row, argsIn.SourceMap["Authors"]].Value),
                        Genres = GetCollection<GenreDto>(worksheet.Cells[row, argsIn.SourceMap["Genres"]].Value)
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

        private T GetSimple<T>(object cellValue)
        {
            return (T)Convert.ChangeType(cellValue, typeof(T));
        }

        private T GetDto<T>(object cellValue)
            where T : Dto
        {
            var repository = _unitOfWork.GetRepository<T>();
            return repository.FindBy(dto => dto.Name == cellValue.ToString()).SingleOrDefault();
        }

        private ICollection<T> GetCollection<T>(object cellValue)
            where T : Dto
        {
            var repository = _unitOfWork.GetRepository<T>();
            var importedValues = cellValue.ToString().Split(',').ToList();
            var collection = new List<Dto>(importedValues.Count);

            importedValues.ForEach(importedValue =>
            {
                importedValue.Trim();
                var possibleEntry = repository.FindBy(dto => dto.Name == importedValue).SingleOrDefault();
                if (possibleEntry != null)
                {
                    collection.Add(possibleEntry);
                }
            });

            return (ICollection<T>)collection;
        }
    }
}