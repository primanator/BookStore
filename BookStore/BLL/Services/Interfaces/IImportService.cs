namespace BLL.Services.Interfaces
{
    using System.IO;

    public interface IImportService
    {
        void Import(Stream source);
    }
}