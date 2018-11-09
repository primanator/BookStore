namespace BLL.Services.Interfaces
{
    using System.IO;

    internal interface IImportService
    {
        void Import(Stream source);
    }
}