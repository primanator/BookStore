namespace BLL.Factory.Implementation.Excel.Books
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Models;
    using Interfaces;
    using OfficeOpenXml;
    using Contracts.Models;
    using Contracts.Utils;

    internal class BookExcelValidator : IValidator
    {
        public event SuccessfulValidationHandler<ValidationEventArgs> ValidatonPassed;

        private readonly Dictionary<string, int> _propertyColumnDictionary;
        private string[] _missingProperties;
        private int _newBooksAmount;

        public BookExcelValidator()
        {
            var dtoProperties = typeof(Book).GetProperties()
                .Where(prop => !prop.Name.Contains("Id"))
                .ToList();

            _propertyColumnDictionary = new Dictionary<string, int>(dtoProperties.Count);
            dtoProperties.ForEach(prop => _propertyColumnDictionary.Add(prop.Name.ToLowerInvariant(), 0));
        }

        public void Validate(Stream srcStream)
        {
            var failReason = string.Empty;
            if (!HeaderChecked(srcStream, ref failReason) || !ContentChecked(srcStream, ref failReason))
            {
                throw new FailedImportException(srcStream, failReason);
            }

            var args = new ValidationEventArgs
            {
                SourceStream = srcStream,
                SourceMap = _propertyColumnDictionary,
                EntriesAmount = _newBooksAmount
            };
            ValidatonPassed?.Invoke(this, args);
        }

        private bool HeaderChecked(Stream srcStream, ref string failReason)
        {
            var package = new ExcelPackage(srcStream);

            if (package.Workbook.Worksheets.Count == 0)
            {
                failReason = "Excel file contains no workheets.";
                return false;
            }
            if (package.Workbook.Worksheets.First().Dimension == null)
            {
                failReason = "Workheets contains no cells.";
                return false;
            }
            if (PropertiesAndColumnsDoNotMatch(package))
            {
                MarkMissingHeaderPropertiesInFile(package);
                failReason = "Not all properties found in header.";
                return false;
            }
            package.Save();

            return true;
        }

        private bool ContentChecked(Stream srcStream, ref string failReason)
        {
            var contentIsOk = true;

            using (var package = new ExcelPackage(srcStream))
            {
                var worksheet = package.Workbook.Worksheets.First();
                for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (!RowContainsValue(worksheet, row))
                    {
                        continue;
                    }

                    var dataValidationPassed = CheckName(worksheet, worksheet.Cells[row, _propertyColumnDictionary["name"]].Address)
                                               | CheckIsbn(worksheet, worksheet.Cells[row, _propertyColumnDictionary["isbn"]].Address)
                                               | CheckPages(worksheet, worksheet.Cells[row, _propertyColumnDictionary["pages"]].Address)
                                               | CheckLimitedEdition(worksheet, worksheet.Cells[row, _propertyColumnDictionary["limitededition"]].Address)
                                               | CheckWrittenIn(worksheet, worksheet.Cells[row, _propertyColumnDictionary["writtenin"]].Address)
                                               | CheckLibrary(worksheet, worksheet.Cells[row, _propertyColumnDictionary["library"]].Address)
                                               | CheckAuthors(worksheet, worksheet.Cells[row, _propertyColumnDictionary["authors"]].Address)
                                               | CheckGenres(worksheet, worksheet.Cells[row, _propertyColumnDictionary["genres"]].Address);
                    if (dataValidationPassed)
                    {
                        _newBooksAmount++;
                    }
                    else
                    {
                        failReason = "There is a mistake in content of the import file. Please review recieved copy.";
                        contentIsOk = false;
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
                var cellText = worksheet.Cells[row, column].Text.Trim();
                if (!string.IsNullOrEmpty(cellText))
                {
                    valueFound = true;
                    break;
                }
            }

            return valueFound;
        }

        private bool PropertiesAndColumnsDoNotMatch(ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.First();

            for (int column = worksheet.Dimension.Start.Column; column <= worksheet.Dimension.End.Column; column++)
            {
                var cellText = worksheet.Cells[1, column].Text.Trim().ToLowerInvariant();

                if (_propertyColumnDictionary.ContainsKey(cellText))
                {
                    _propertyColumnDictionary.Remove(cellText);
                    _propertyColumnDictionary.Add(cellText, column);
                }
            }

            _missingProperties = _propertyColumnDictionary.Keys.Where(propertyName => _propertyColumnDictionary[propertyName] == 0).ToArray();
            return _missingProperties.Length > 0;
        }

        private void MarkMissingHeaderPropertiesInFile(ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.First();
            for (int column = worksheet.Dimension.End.Column + 1, propertyIndex = 0; propertyIndex < _missingProperties.Length; column++)
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
            if (Regex.IsMatch(worksheet.Cells[address].Text.Trim(), (@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$")))
            {
                return true;
            }

            AddCellValidation(worksheet, address, "Invalid Name content", "Name has to not contain special characters");
            return false;
        }

        // Matches 0672317249 | 9780672317248 
        //Non-Matches 0-672-31724-9 | 5555555555555
        private bool CheckIsbn(ExcelWorksheet worksheet, string address)
        {
            if (Regex.IsMatch(worksheet.Cells[address].Text.Trim(), (@"^(97(8|9))?\d{9}(\d|X)$")))
            {
                return true;
            }

            AddCellValidation(worksheet, address, "Invalid ISBN content", "ISBN has to consist of 10 or 13 numberical digits");
            return false;
        }

        private bool CheckPages(ExcelWorksheet worksheet, string address)
        {
            if (int.TryParse(worksheet.Cells[address].Text.Trim(), out var cellValue) && cellValue >= 0)
            {
                return true;
            }

            AddCellValidation(worksheet, address, "Invalid Pages content", "Pages value has to be numerical that is greater than zero");
            return false;
        }

        private bool CheckGenres(ExcelWorksheet worksheet, string address)
        {
            var genresAreOk = true;

            foreach (var possibleGenre in worksheet.Cells[address].Text.Trim().Split(','))
            {
                if (Regex.IsMatch(possibleGenre, @"^[\sA-Za-z0-9]$"))
                {
                    AddCellValidation(worksheet, address, "Invalid Genres content", "Genres value has to be a comma separated list of single-word genres");
                    genresAreOk = false;
                }
            }
            return genresAreOk;
        }

        // Matches T.F. Johnson | John O'Neil | Mary-Kate Johnson
        // Non-Matches sam_johnson | Joe--Bob Jones | dfjsd0rd
        private bool CheckAuthors(ExcelWorksheet worksheet, string address)
        {
            var authorsAreOk = true;

            foreach (var possibleAuthor in worksheet.Cells[address].Text.Trim().Split(','))
            {
                if (!Regex.IsMatch(possibleAuthor, @"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$"))
                {
                    AddCellValidation(worksheet, address, "Invalid Authors content", "Authors value has to be a comma separated list of alike names T.F. Johnson | John O'Neil | Mary-Kate Johnson");
                    authorsAreOk = false;
                }
            }
            return authorsAreOk;
        }

        private bool CheckLibrary(ExcelWorksheet worksheet, string address)
        {
            if (Regex.IsMatch(worksheet.Cells[address].Text.Trim(), @"^[A-Za-z\ ]$"))
            {
                return true;
            }
            AddCellValidation(worksheet, address, "Invalid Library content", "Library value has to be a single word with no special characters except whitespace");
            return false;
        }

        // Matches 05/01/2009 | 2009-05-01 | 1 May 2008 | Fri, 15 May 2009
        // Non-Matches 16-05-2009
        private bool CheckWrittenIn(ExcelWorksheet worksheet, string address)
        {
            if (DateTime.TryParse(worksheet.Cells[address].Text.Trim(), out var date))
            {
                return true;
            }
            AddCellValidation(worksheet, address, "Invalid Written In content", "Written In value have to be in readable datetime format (dd/mm/yyyy)");
            return false;
        }

        // Matches 1, 0, true, false, t or f
        private bool CheckLimitedEdition(ExcelWorksheet worksheet, string address)
        {
            if (Regex.IsMatch(worksheet.Cells[address].Text.Trim(), @"^((true)|(false)|(t)|(f)|(0)|(1))$"))
            {
                return true;
            }
            AddCellValidation(worksheet, address, "Invalid Limited Edition content", "Limited Edition value has to be 1, 0, true, false, t or f");
            return false;
        }
    }
}