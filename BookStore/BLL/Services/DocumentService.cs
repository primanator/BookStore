namespace BLL.Services
{
    using BLL.Interfaces;
    using DAL.Interfaces;
    using System;
    using System.IO;

    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void PerformImport(Stream update)
        {
            throw new NotImplementedException("Import through .xlsx file is not yet implemented.");
        }
    }
}
