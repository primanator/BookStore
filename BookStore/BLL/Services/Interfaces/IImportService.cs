namespace BLL.Services.Interfaces
{
    using System.Web;

    public interface IImportService
    {
        HttpPostedFile Import(HttpPostedFile source);
    }
}