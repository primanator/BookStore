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
                SourceMap = _propertyColumnDictionary
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
                else if (package.Workbook.Worksheets.FirstOrDefault().Dimension == null)
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
                    failReason = "Not all properties found.";
                    return false;
                }
            }
            failReason = "There is a mistake in content of the import file. Please review recieved copy.";
            return true;
        }

        private bool CheckContent(Stream srcStream)
        {
            using (var package = new ExcelPackage(srcStream))
            {
                //
            }
            return true;
        }

        private bool PropertiesAndColumnsMatch(ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            for (int column = worksheet.Dimension.Start.Column; column <= worksheet.Dimension.End.Column; column++)
            {
                var cellValue = worksheet.Cells[1, column].Value;
                var cellString = cellValue != null ? cellValue.ToString().Trim() : string.Empty;

                if (_propertyColumnDictionary.TryGetValue(cellString, out var columnNumber))
                {
                    if (columnNumber > 0)
                    {
                        return false;
                    }

                    _propertyColumnDictionary.Remove(cellString);
                    _propertyColumnDictionary.Add(cellString, column);
                }
            }
            return true;
        }

        private bool PropertiesMissing()
        {
            // in excel column indexes start at 1, so if any column number is 0 - corresponding property is missing in import file
            return _propertyColumnDictionary.Keys.Any(propertyName =>
            {
                return _propertyColumnDictionary[propertyName] == 0;
            });
        }
    }
}