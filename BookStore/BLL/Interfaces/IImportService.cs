namespace BLL.Interfaces
{
    using DTO.Entities;
    using System.Web;

    public interface IImportService
    {
        HttpPostedFile Execute<T>(HttpPostedFile importSource, IValidator<T> validator) where T : Dto;
    }
}