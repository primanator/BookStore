namespace BLL.Factory.Implementation
{
    using Services.Implementation;
    using Services.Interfaces;
    using DAL.Interfaces;
    using Interfaces;
    using BLL.Factory.Implementation.Book;
    using BLL.Factory.Implementation.Book.Excel;

    public class ImportServiceFactory : IImportServiceFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImportServiceFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IImportService GetBookImportService()
        {
            return new ImportService(new BookDtoExcelValidator(), new BookDtoExcelExtractor(), new BookDtoImporter(_unitOfWork));
        }
    }
}