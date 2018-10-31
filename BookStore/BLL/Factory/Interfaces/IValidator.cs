namespace BLL.Factory.Interfaces
{
    using System.Web;

    public interface IValidator
    {
        bool CheckStructure(HttpPostedFile source, out string failReason);

        bool CheckContent(HttpPostedFile source);
    }
}