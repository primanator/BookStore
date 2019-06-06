namespace BLL.Factory.Interfaces
{
    using Services.Interfaces;

    public interface IImportServiceFactory
    {
        IImportService GetBookImportService();
    }
}