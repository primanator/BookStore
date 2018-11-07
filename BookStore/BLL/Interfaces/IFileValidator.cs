namespace BLL.Interfaces
{
    using DTO.Entities;
    using System.Web;

    public interface IFileValidator<T>
        where T : Dto
    {
        bool CheckStructure(HttpPostedFile source, out string failReason);

        bool CheckContent(HttpPostedFile source, out string failReason);
    }
}