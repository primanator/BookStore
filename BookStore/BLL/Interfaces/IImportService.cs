namespace BLL.Interfaces
{
    using DTO.Entities;
    using System.Web;

    public interface IImportService
    {
        void Execute<T>(HttpPostedFile importSource, IValidator<T> validator) where T : Dto;
    }
}