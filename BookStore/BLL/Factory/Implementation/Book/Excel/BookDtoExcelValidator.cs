namespace BLL.Factory.Implementation.Book.Excel
{
    using System;
    using System.Web;
    using Interfaces;

    public class BookDtoExcelValidator : IValidator
    {
        public bool CheckStructure(HttpPostedFile source, out string failReason)
        {
            throw new NotImplementedException();
        }

        public bool CheckContent(HttpPostedFile source)
        {
            throw new NotImplementedException();
        }
    }
}