namespace BLL.Services.Interfaces
{
    using System.IO;

    public interface IImportService
    {
        Stream Import(Stream source);
    }
}