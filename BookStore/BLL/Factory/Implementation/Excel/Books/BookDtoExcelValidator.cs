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
        private enum AlphabetLetters
        {
            A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
        };

        public event SuccessfulValidationHandler<ValidationEventArgs> ValidatonPassed;

        private Dictionary<string, int> _propertyColumnDictionary;

        public BookDtoExcelValidator()
        {
            var dtoProperties = typeof(BookDto).GetProperties()
                .Where(prop => !prop.Name.Contains("Id"))
                .ToList();

            dtoProperties.ForEach(prop => _propertyColumnDictionary.Add(prop.Name, 0));
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
                EntriesAmount = 1 // calculate when validating content
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
            throw new NotImplementedException();
        }

        private bool PropertiesAndColumnsMatch(ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.First();
            for (int column = worksheet.Dimension.Start.Column; column <= worksheet.Dimension.End.Column; column++)
            {
                var cellValue = worksheet.Cells[1, column].Text.Trim();

                if (_propertyColumnDictionary.TryGetValue(cellValue, out var columnNumber))
                {
                    if (columnNumber > 0)
                    {
                        return false;
                    }

                    _propertyColumnDictionary.Remove(cellValue);
                    _propertyColumnDictionary.Add(cellValue, column);
                }
            }
            return true;
        }

        private bool PropertiesMissing()
        {
            return _propertyColumnDictionary.Keys.Any(propertyName =>
            {
                // in excel column indexes start at 1, so if any column number is 0 - corresponding property is missing in import file
                return _propertyColumnDictionary[propertyName] == 0;
            });
        }

        private void MarkMissingProperties(ExcelPackage package)
        {
            var missingProperties = _propertyColumnDictionary.Keys.Where(propertyName =>
            {
                return _propertyColumnDictionary[propertyName] == 0;
            }).ToArray();

            var worksheet = package.Workbook.Worksheets.First();
            for (int column = worksheet.Dimension.Start.Column, propertyIndex = 0; column <= worksheet.Dimension.End.Column; column++)
            {
                var cellValue = worksheet.Cells[1, column].Text.Trim();
                if (string.IsNullOrEmpty(cellValue))
                {
                    var columnLetter = ((AlphabetLetters)column - 1); // in excel column indexes start at 1, but in enum - from 0
                    var cell = worksheet.DataValidations.AddAnyValidation($"A:{columnLetter}");
                    cell.ShowErrorMessage = true;
                    cell.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop;
                    cell.ErrorTitle = "Book property is missing";
                    cell.Error = $"Please provide value for {missingProperties[propertyIndex]}";
                    cell.AllowBlank = false;

                    propertyIndex++;
                }
            }
        }
    }
}