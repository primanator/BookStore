namespace BLL.Interfaces
{
    using System.IO;

    public interface IDocumentService
    {
        void PerformImport(Stream update);
    }
}
