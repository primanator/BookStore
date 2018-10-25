namespace BLL.Interfaces
{
    using DTO.Entities;
    using System.Collections.Generic;
    using System.Web;

    public interface IValidator
    {
        bool Check(HttpPostedFile importSource, out string failReason);
    }

    public interface IValidator<T> : IValidator
        where T : Dto
    {
        List<T> Extract(HttpPostedFile importSource);
    }
}