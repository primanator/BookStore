namespace BLL.Factory.Implementation.Excel.Books
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using BLL.Models;
    using DTO.Entities;
    using Interfaces;
    using OfficeOpenXml;

    internal class BookDtoExcelValidator : IValidator
    {
        public event SuccessfulValidationHandler<ValidationEventArgs> ValidatonPassed;

        private Dictionary<string, int> _propertyColumnDictionary;
        private string[] _missingProperties;
        private int _newBooksAmount;

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
                EntriesAmount = _newBooksAmount
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
                else if (HeaderPropertiesMissing())
                {
                    MarkMissingHeaderPropertiesInFile(package);
                    failReason = "Not all properties found in header.";
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
                        if (CheckName(worksheet, worksheet.Cells[row, _propertyColumnDictionary["name"]].Address)
                            && CheckIsbn(worksheet, worksheet.Cells[row, _propertyColumnDictionary["isbn"]].Address)
                            && CheckPages(worksheet, worksheet.Cells[row, _propertyColumnDictionary["pages"]].Address)
                            && CheckLimitedEdition(worksheet, worksheet.Cells[row, _propertyColumnDictionary["limitededition"]].Address)
                            && CheckWrittenIn(worksheet, worksheet.Cells[row, _propertyColumnDictionary["writtenin"]].Address)
                            && CheckLibrary(worksheet, worksheet.Cells[row, _propertyColumnDictionary["library"]].Address)
                            && CheckAuthors(worksheet, worksheet.Cells[row, _propertyColumnDictionary["authors"]].Address)
                            && CheckGenres(worksheet, worksheet.Cells[row, _propertyColumnDictionary["genres"]].Address))
                        {
                            _newBooksAmount++;
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

        private bool HeaderPropertiesMissing()
        {
            _missingProperties = _propertyColumnDictionary.Keys.Where(propertyName =>
            {
                // in excel column indexes start at 1, so if any column number is 0 - corresponding property is missing in import file
                return _propertyColumnDictionary[propertyName] == 0;
            }).ToArray();

            return _missingProperties.Length > 0;
        }

        private void MarkMissingHeaderPropertiesInFile(ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.First();
            for (int column = worksheet.Dimension.Start.Column, propertyIndex = 0; column <= worksheet.Dimension.End.Column; column++)
            {
                var cell = worksheet.Cells[1, column];
                if (string.IsNullOrEmpty(cell.Text.Trim()))
                {
                    AddCellValidation(worksheet, worksheet.Cells[1, column].Address, "Book property is missing", $"Please provide value for {_missingProperties[propertyIndex]}");
                    propertyIndex++;
                }
            }
        }

        private void AddCellValidation(ExcelWorksheet worksheet, string address, string errorTitle, string errorMessage)
        {
            var cellValidataion = worksheet.DataValidations.AddAnyValidation(address);

            cellValidataion.ShowErrorMessage = true;
            cellValidataion.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop;
            cellValidataion.ErrorTitle = errorTitle;
            cellValidataion.Error = errorMessage;
            cellValidataion.AllowBlank = false;
        }
        private bool CheckName(ExcelWorksheet worksheet, string address)
        {
            var cellText = worksheet.Cells[address].Text.Trim();
            if (!Regex.IsMatch(cellText, (@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$")))
            {
                AddCellValidation(worksheet, address, "Invalid Name content", "Name has to not contain special characters");
                return false;
            }
            return true;
        }

        private bool CheckIsbn(ExcelWorksheet worksheet, string address)
        {
            var cellText = worksheet.Cells[address].Text.Trim();
            if (!Regex.IsMatch(cellText, (@"^(97(8|9))?\d{9}(\d|X)$")))
            {
                AddCellValidation(worksheet, address, "Invalid ISBN content", "ISBN has to consist of 10 or 13 numberical digits");
                return false;
            }
            return true;
        }

        private bool CheckPages(ExcelWorksheet worksheet, string address)
        {
            var cellText = worksheet.Cells[address].Text.Trim();
            if (Int32.TryParse(cellText, out var cellValue) || cellValue < 0)
            {
                AddCellValidation(worksheet, address, "Invalid Pages content", "Pages value has to be numerical that is greater than zero");
                return false;
            }
            return true;
        }

        private bool CheckGenres(ExcelWorksheet worksheet, string address)
        {
            var cellText = worksheet.Cells[address].Text.Trim();
            foreach (var possibleGenre in Regex.Matches(cellText, @"(.+?)(?:,|$)").OfType<string>())
            {
                if (!Regex.IsMatch(possibleGenre, @"^[\sA-Za-z]$"))
                {
                    AddCellValidation(worksheet, address, "Invalid Genres content", "Genres value has to be a comma separated list of single-word genres");
                    return false;
                }
            }
            return true;
        }

        private bool CheckAuthors(ExcelWorksheet worksheet, string address)
        {
            var cellText = worksheet.Cells[address].Text.Trim();
            foreach (var possibleAuthor in Regex.Matches(cellText, @"(.+?)(?:,|$)").OfType<string>())
            {
                if (!Regex.IsMatch(possibleAuthor, @"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$")) // Non-Matches sam_johnson | Joe--Bob Jones | dfjsd0rd
                {
                    AddCellValidation(worksheet, address, "Invalid Authors content", "Authors value has to be a comma separated list of alike names T.F. Johnson | John O'Neil | Mary-Kate Johnson");
                    return false;
                }
            }
            return true;
        }

        private bool CheckLibrary(ExcelWorksheet worksheet, string address)
        {
            var cellText = worksheet.Cells[address].Text.Trim();
            if (!Regex.IsMatch(cellText, @"^[\sA-Za-z]$"))
            {
                AddCellValidation(worksheet, address, "Invalid Library content", "Library value has to be a single word with no special characters");
                return false;
            }
            return true;
        }

        private bool CheckWrittenIn(ExcelWorksheet worksheet, string address)
        {
            var cellText = worksheet.Cells[address].Text.Trim();
            if (!Regex.IsMatch(cellText, @"^([0]?[1-9]|[1][0-2])[./-]([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0-9]{4}|[0-9]{2})$"))
            {
                AddCellValidation(worksheet, address, "Invalid Written In content", "Written In value has to have dd/mm/yyyy format");
                return false;
            }
            return true;
        }

        private bool CheckLimitedEdition(ExcelWorksheet worksheet, string address)
        {
            var cellText = worksheet.Cells[address].Text.Trim();
            if (!Regex.IsMatch(cellText, @"^((true)|(false)|(t)|(f)|(0)|(1)))$"))
            {
                AddCellValidation(worksheet, address, "Invalid Limited Edition content", "Limited Edition value has to be 1, 0, true, false, t or f");
                return false;
            }
            return true;
        }
    }
}