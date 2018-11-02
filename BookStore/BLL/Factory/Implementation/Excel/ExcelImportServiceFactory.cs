namespace BLL.Factory.Implementation.Excel
{
    using Services.Implementation;
    using Services.Interfaces;
    using DAL.Interfaces;
    using Interfaces;
    using BLL.Factory.Implementation.Excel.Books;

    public class ExcelImportServiceFactory : IImportServiceFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExcelImportServiceFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IImportService GetBookImportService()
        {
            return new ImportService(new BookDtoExcelValidator(), new BookDtoExcelExtractor(_unitOfWork), new BookDtoImporter(_unitOfWork));
        }
    }
}