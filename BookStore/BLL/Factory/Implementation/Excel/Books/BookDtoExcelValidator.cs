namespace BLL.Factory.Implementation.Excel.Books
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BLL.Models;
    using DTO.Entities;
    using Interfaces;
    using OfficeOpenXml;

    internal class BookDtoExcelValidator : IValidator
    {
        public event SuccessfulValidationHandler<ValidationEventArgs> ValidatonPassed;

        private Dictionary<string, int> _propertyColumnDictionary;
        private string[] _missingProperties;
        private int _dtosAmount; // calculate when validating content

        public BookDtoExcelValidator()
        {
            var dtoProperties = typeof(BookDto).GetProperties()
                .Where(prop => !prop.Name.Contains("Id"))
                .ToList();

            dtoProperties.ForEach(prop => _propertyColumnDictionary.Add(prop.Name.ToLowerInvariant(), 0));
        }

        public void Validate(Stream srcStream)
        {
            if (!CheckStructure(srcStream, out string failReason) || !CheckContent(srcStream))
            {
                throw new FormatException(failReason);
            }

            var args = new ValidationEventArgs
            {
                SourceStream = srcStream,
                SourceMap = _propertyColumnDictionary,
                EntriesAmount = _dtosAmount
            };
            ValidatonPassed?.Invoke(this, args);
        }

        private bool CheckStructure(Stream srcStream, out string failReason)
        {
            using (var package = new ExcelPackage(srcStream))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    failReason = "Excel file contains no workheets.";
                    return false;
                }
                else if (package.Workbook.Worksheets.First().Dimension == null)
                {
                    failReason = "Workheets contains no cells.";
                    return false;
                }
                else if (!PropertiesAndColumnsMatch(package))
                {
                    failReason = "Redundant properties found.";
                    return false;
                }
                else if (PropertiesMissing())
                {
                    MarkMissingProperties(package);
                    failReason = "Not all properties found.";
                    return false;
                }
            }
            failReason = "There is a mistake in content of the import file. Please review recieved copy.";
            return true;
        }

        private bool CheckContent(Stream srcStream)
        {
            var contentIsOk = true;

            using (var package = new ExcelPackage(srcStream))
            {
                var worksheet = package.Workbook.Worksheets.First();
                for (int row = worksheet.Dimension.Start.Row; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (RowContainsValue(worksheet, row))
                    {
                        if (CheckIsbn(worksheet, row, _propertyColumnDictionary["isbn"])
                            && CheckPages(worksheet, row, _propertyColumnDictionary["pages"])
                            && CheckLimitedEdition(worksheet, row, _propertyColumnDictionary["limitededition"])
                            && CheckWrittenIn(worksheet, row, _propertyColumnDictionary["writtenin"])
                            && CheckLibrary(worksheet, row, _propertyColumnDictionary["library"])
                            && CheckAuthors(worksheet, row, _propertyColumnDictionary["authors"])
                            && CheckGenres(worksheet, row, _propertyColumnDictionary["genres"]))
                        {
                            _dtosAmount++;
                        }
                        else
                        {
                            contentIsOk = false;
                        }
                    }
                }
            }

            return contentIsOk;
        }

        private bool RowContainsValue(ExcelWorksheet worksheet, int row)
        {
            var valueFound = false;

            for (int column = worksheet.Dimension.Start.Column; column <= worksheet.Dimension.End.Column; column++)
            {
                var cellText = worksheet.Cells[1, column].Text.Trim();
                if (!string.IsNullOrEmpty(cellText))
                {
                    valueFound = true;
                    break;
                }
            }

            return valueFound;
        }

        private bool PropertiesAndColumnsMatch(ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.First();

            for (int column = worksheet.Dimension.Start.Column; column <= worksheet.Dimension.End.Column; column++)
            {
                var cellText = worksheet.Cells[1, column].Text.Trim().ToLowerInvariant();

                if (_propertyColumnDictionary.TryGetValue(cellText, out var columnNumber))
                {
                    if (columnNumber > 0)
                    {
                        return false;
                    }

                    _propertyColumnDictionary.Remove(cellText);
                    _propertyColumnDictionary.Add(cellText, column);
                }
            }

            return true;
        }

        private bool PropertiesMissing()
        {
            _missingProperties = _propertyColumnDictionary.Keys.Where(propertyName =>
            {
                // in excel column indexes start at 1, so if any column number is 0 - corresponding property is missing in import file
                return _propertyColumnDictionary[propertyName] == 0;
            }).ToArray();

            return _missingProperties.Length > 0;
        }

        private void MarkMissingProperties(ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.First();
            for (int column = worksheet.Dimension.Start.Column, propertyIndex = 0; column <= worksheet.Dimension.End.Column; column++)
            {
                var cell = worksheet.Cells[1, column];
                if (string.IsNullOrEmpty(cell.Text.Trim()))
                {
                    var cellValidataion = worksheet.DataValidations.AddAnyValidation(cell.Address);

                    cellValidataion.ShowErrorMessage = true;
                    cellValidataion.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop;
                    cellValidataion.ErrorTitle = "Book property is missing";
                    cellValidataion.Error = $"Please provide value for {_missingProperties[propertyIndex]}";
                    cellValidataion.AllowBlank = false;

                    propertyIndex++;
                }
            }
        }

        private bool CheckIsbn(ExcelWorksheet worksheet, int row, int column)
        {
            return false;
        }

        private bool CheckPages(ExcelWorksheet worksheet, int row, int column)
        {
            return false;
        }

        private bool CheckGenres(ExcelWorksheet worksheet, int row, int column)
        {
            return false;
        }

        private bool CheckAuthors(ExcelWorksheet worksheet, int row, int column)
        {
            return false;
        }

        private bool CheckLibrary(ExcelWorksheet worksheet, int row, int column)
        {
            return false;
        }

        private bool CheckWrittenIn(ExcelWorksheet worksheet, int row, int column)
        {
            return false;
        }

        private bool CheckLimitedEdition(ExcelWorksheet worksheet, int row, int column)
        {
            return false;
        }
    }
}