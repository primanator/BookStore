namespace BLL.Factory.Implementation.Excel
{
    using Services.Implementation;
    using Services.Interfaces;
    using DAL.Interfaces;
    using Interfaces;
    using BLL.Factory.Implementation.Excel.Books;
    using System.Collections.Generic;
    using System;
    using DTO.Entities;

    public class ExcelImportServiceFactory : IImportServiceFactory
    {
        private readonly IUnitOfWork _unitOfWork;
        private Dictionary<Type, ImportService> _servicesDictionary;

        public ExcelImportServiceFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _servicesDictionary = new Dictionary<Type, ImportService>();
        }

        public IImportService GetBookImportService()
        {
            var serviceType = typeof(BookDto);

            if (_servicesDictionary.TryGetValue(serviceType, out var importService))
                return importService;

            importService = new ImportService(new BookDtoExcelValidator(), new BookDtoExcelExtractor(_unitOfWork), new BookDtoImporter(_unitOfWork));
            _servicesDictionary.Add(serviceType, importService);

            return importService;
        }
    }
}