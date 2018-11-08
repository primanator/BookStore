namespace BLL.Factory.Implementation.Excel.Books
{
    using System;
    using BLL.Factory.Interfaces;
    using BLL.Models;
    using DAL.Interfaces;
    using DTO.Entities;

    internal class BookDtoImporter : IImporter
    {
        private readonly IUnitOfWork _unitOfWork;

        public event EventHandler ImportPassed;

        public BookDtoImporter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void Import(object sender, ExtractionEventArgs args)
        {
            var repository = _unitOfWork.GetRepository<BookDto>();
            importData.ForEach(newItem => repository.Insert((BookDto)newItem));
            _unitOfWork.Save();
        }
    }
}