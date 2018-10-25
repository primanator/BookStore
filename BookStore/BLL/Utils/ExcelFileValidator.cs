namespace BLL.Utils
{
    using BLL.Interfaces;
    using DTO.Entities;
    using OfficeOpenXml;
    using System.Collections.Generic;
    using System.Web;
    using System.Linq;
    using System;
    using System.Collections.ObjectModel;

    public class ExcelFileValidator<T> : IValidator<T>, IValidator
        where T : Dto
    {
        private Dictionary<string, int> _propertyColumnDictionary;

        public ExcelFileValidator()
        {
            var dtoProperties = typeof(T).GetProperties()
                .Where(prop => !prop.Name.Contains("Id"))
                .ToList();

            dtoProperties.ForEach(prop => _propertyColumnDictionary.Add(prop.Name, 0));
        }

        public bool Check(HttpPostedFile importSource, out string failReason)
        {
            if (CheckFileStructure(importSource, out failReason))
            {
                return CheckFileContent(importSource);
            }
            return false;
        }

        public List<T> Extract(HttpPostedFile importSource)
        {
            return new List<T>();
        }

        private bool CheckFileStructure(HttpPostedFile importSource, out string failReason)
        {
            using (var package = new ExcelPackage(importSource.InputStream))
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
            failReason = string.Empty;
            return true;
        }

        private bool CheckFileContent(HttpPostedFile importSource)
        {
            using (var package = new ExcelPackage(importSource.InputStream))
            {
                var dtoType = typeof(T);
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                _propertyColumnDictionary.Keys.ToList().ForEach(dtoProperyName =>
                {
                    var columnNumber = _propertyColumnDictionary[dtoProperyName];
                    var propertyType = dtoType.GetProperty(dtoProperyName).PropertyType;
                    var typeCode = Type.GetTypeCode(propertyType);

                    switch (typeCode)
                    {
                        case TypeCode.Int32:
                            {
                                ValidateIntegerColumn(worksheet, columnNumber);
                                break;
                            }
                        case TypeCode.String:
                            {
                                ValidateStringColumn(worksheet, columnNumber);
                                break;
                            }
                        case TypeCode.DateTime:
                            {
                                ValidateDateColumn(worksheet, columnNumber);
                                break;
                            }
                        case TypeCode.Object:
                            {
                                if (propertyType.IsGenericType && typeof(Collection<>).IsAssignableFrom(propertyType.GetGenericTypeDefinition()))
                                {
                                    ValidateListColumn(worksheet, columnNumber);
                                }
                                else
                                {
                                    //validate object
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                });
            }
            return true;
        }

        private void ValidateIntegerColumn(ExcelWorksheet worksheet, int columnNumber)
        {

        }

        private void ValidateStringColumn(ExcelWorksheet worksheet, int columnNumber)
        {

        }

        private void ValidateDateColumn(ExcelWorksheet worksheet, int column)
        {

        }

        private void ValidateListColumn(ExcelWorksheet worksheet, int columnNumber)
        {

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
