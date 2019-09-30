namespace BLL.Factory.Implementation.Excel.Books
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Interfaces;
    using DAL.Interfaces;
    using OfficeOpenXml;
    using Models;
    using Contracts.Models;

    internal class BookExcelExtractor : IExtractor
    {
        public event ExtractHandler<ExtractionEventArgs> ExtractionPassed;

        private readonly IUnitOfWork _unitOfWork;

        public BookExcelExtractor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Extract(object sender, ValidationEventArgs argsIn)
        {
            var newData = new List<BaseContract>(argsIn.EntriesAmount);

            using (var package = new ExcelPackage(argsIn.SourceStream))
            {
                var worksheet = package.Workbook.Worksheets.First();
                for (var row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    newData.Add(new Book
                    {
                        Name = GetSimple<string>(worksheet.Cells[row, argsIn.SourceMap["name"]].Value),
                        Isbn = GetSimple<string>(worksheet.Cells[row, argsIn.SourceMap["isbn"]].Value),
                        Pages = GetSimple<int>(worksheet.Cells[row, argsIn.SourceMap["pages"]].Value),
                        LimitedEdition = GetSimple<bool>(worksheet.Cells[row, argsIn.SourceMap["limitededition"]].Value),
                        WrittenIn = GetSimple<DateTime>(worksheet.Cells[row, argsIn.SourceMap["writtenin"]].Text),
                        Library = GetContract<Library>(dto => dto.Id == 1), // only one library is present for now (newItem.Library.Id)
                        Authors = GetCollection<Author>(worksheet.Cells[row, argsIn.SourceMap["authors"]].Value),
                        Genres = GetCollection<Genre>(worksheet.Cells[row, argsIn.SourceMap["genres"]].Value)
                    });
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

        private T GetContract<T>(Func<T, bool> func)
            where T : BaseContract
        {
            var repository = _unitOfWork.GetRepository<T>();
            return repository.FindBy(x => func(x)).SingleOrDefault();
        }

        private ICollection<T> GetCollection<T>(object cellValue)
            where T : BaseContract
        {
            var repository = _unitOfWork.GetRepository<T>();
            var importedValues = cellValue.ToString().Split(',').ToList();
            var collection = new List<T>(importedValues.Count);

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