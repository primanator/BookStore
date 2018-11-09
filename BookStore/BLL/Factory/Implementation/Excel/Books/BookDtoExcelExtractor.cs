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

            if (cellType.IsGenericType) // dto collections
            {
                return GetCollection<T>(cellType, cellValue);
            }
            else if (cellType == typeof(Dto)) // other dto properies
            {
                return GetDtoProperty<T>(cellType, cellValue);
            }
            else // primitive dto properies
            {
                return (T)Convert.ChangeType(cellValue, cellType);
            }
        }

        private T GetCollection<T>(Type cellType, object cellValue)
        {
            var parsedValues = cellValue.ToString().Split(',').ToList();
            parsedValues.ForEach(str => str.Trim());
            var collectionType = cellType.GetGenericArguments().SingleOrDefault();

            if (collectionType == typeof(AuthorDto))
            {
                var authors = new List<AuthorDto>(parsedValues.Count);
                var authorsRepository = _unitOfWork.GetRepository<AuthorDto>();

                parsedValues.ForEach(authorName =>
                {
                    var possibleAuthor = authorsRepository.FindBy(author => author.Name == authorName).SingleOrDefault();
                    if (possibleAuthor != null)
                        authors.Add(possibleAuthor);
                });

                return (T)Convert.ChangeType(authors, cellType);
            }
            else if (collectionType == typeof(GenreDto))
            {
                var genres = new List<GenreDto>();
                var genresRepository = _unitOfWork.GetRepository<GenreDto>();

                parsedValues.ForEach(genreName =>
                {
                    var possibleGenre = genresRepository.FindBy(genre => genre.Name == genreName).SingleOrDefault();
                    if (possibleGenre != null)
                        genres.Add(possibleGenre);
                });

                return (T)Convert.ChangeType(genres, cellType);
            }
        }

        private T GetDtoProperty<T>(Type cellType, object cellValue)
        {
            if (cellType == typeof(LibraryDto))
            {
                var libraryRepository = _unitOfWork.GetRepository<LibraryDto>();
                var library = libraryRepository.FindBy(lib => lib.Name == cellValue.ToString());

                if (library != null)
                {
                    return (T)Convert.ChangeType(library, cellType);
                }
            }
        }
    }
}