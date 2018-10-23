namespace BLL.Utils
{
    using BLL.Interfaces;
    using DTO.Entities;
    using OfficeOpenXml;
    using System.Collections.Generic;
    using System.Web;

    public class ExcelFileValidator : IValidator
    {
        private Dictionary<string, int> keyValuePairs; // !

        public ExcelFileValidator()
        {

        }

        public bool Check(HttpPostedFile importSource)
        {
            using (var excelPackage = new ExcelPackage(importSource.InputStream))
            {
                if (excelPackage.Workbook.Worksheets.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public List<T> Extract<T>(HttpPostedFile importSource) where T : Dto
        {
            using (var excelPackage = new ExcelPackage(importSource.InputStream))
            {

            }
            return new List<T>();
        }

        private List<BookDto> DistinguishBooks(ExcelPackage package)
        {
            var newBooks = new List<BookDto>();
            foreach (var worksheet in package.Workbook.Worksheets)
            {
                for (var row = worksheet.Dimension.Start.Row; row <= worksheet.Dimension.End.Row; row++)
                {
                    var possibleEntry = new BookDto();
                    var column = worksheet.Dimension.Start.Column;
                    do
                    {
                        if (worksheet.Cells[row, column].Value != null)
                        {
                            var cellData = worksheet.Cells[row, column].Value.ToString();
                        }
                        else
                        {

                        }
                    } while (column <= 5);
                }
            }
            return new List<BookDto>();
        }

    }
}
