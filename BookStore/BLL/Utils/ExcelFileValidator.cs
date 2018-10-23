namespace BLL.Utils
{
    using BLL.Interfaces;
    using DTO.Entities;
    using OfficeOpenXml;
    using System.Collections.Generic;
    using System.Web;
    using System.Linq;

    public class ExcelFileValidator<T> : IValidator<T>, IValidator
        where T : Dto
    {
        private Dictionary<string, int> _propertyColumnDictionary;

        public ExcelFileValidator()
        {
            var dtoProperties = typeof(T).GetProperties()
                .Where(prop => prop.Name != "Id" && prop.Name != "LibraryId")
                .ToList();

            dtoProperties.ForEach(prop => _propertyColumnDictionary.Add(prop.Name.ToLowerInvariant(), 0)); // in excel column indexes start at 1
        }

        public bool Check(HttpPostedFile importSource)
        {
            using (var package = new ExcelPackage(importSource.InputStream))
            {
                if (package.Workbook.Worksheets.Count == 0
                    | package.Workbook.Worksheets.FirstOrDefault().Dimension == null
                    | !MatchPropertiesAndColumns(package))
                {
                    return false;
                }
            }
            return true;
        }

        public List<T> Extract(HttpPostedFile importSource)
        {
            using (var package = new ExcelPackage(importSource.InputStream))
            {

            }
            return new List<T>();
        }

        private bool MatchPropertiesAndColumns(ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            for (int column = worksheet.Dimension.Start.Column; column <= worksheet.Dimension.End.Column; column++)
            {
                var cellValue = worksheet.Cells[1, column].Value;
                var cellString = cellValue != null ? cellValue.ToString().ToLowerInvariant() : "";

                if (_propertyColumnDictionary.TryGetValue(cellString, out var columnNumber) || columnNumber == 0)
                {

                }
            }

            return _propertyColumnDictionary.Keys.All(propertyName =>
            {
                
                return false;
            });
        }
    }
}
