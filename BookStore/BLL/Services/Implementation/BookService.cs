namespace BLL.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using DAL.Interfaces;
    using Contracts.QueryBuilders;
    using Contracts.Models;

    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookFilterBuilder _filterBuilder;

        public BookService(IUnitOfWork unitOfWork, ContractFilterBuilder<Book> builder)
        {
            if (!(builder is BookFilterBuilder filterBuilder))
                throw new ArgumentException("Can't cast filter builder to Book-oriented");

            _unitOfWork = unitOfWork;
            _filterBuilder = filterBuilder;
        }

        public void Create(Book record)
        {
            if (GetSingle(record.Name) != null)
                throw new ArgumentException("Database already contains book with such name.");

            _unitOfWork.GetRepository<Book>().Insert(record);
            _unitOfWork.Save();
        }

        public void Update(Book record)
        {
            var bookToUpdate = GetSingle(record.Name);
            if (bookToUpdate == null)
                throw new ArgumentException("Database does not contain such book to update.");

            bookToUpdate.SelfUpdate(record);

            _unitOfWork.GetRepository<Book>().Update(bookToUpdate);
            _unitOfWork.Save();
        }

        public IEnumerable<Book> GetAll()
        {
            var predicate = _filterBuilder.FindAll().Build();
            return _unitOfWork.GetRepository<Book>().FindBy(predicate);
        }

        public Book GetSingle(string title)
        {
            var predicate = _filterBuilder.FindByName(title).Build();
            return _unitOfWork.GetRepository<Book>().FindBy(predicate).SingleOrDefault();
        }

        public void Delete(string title)
        {
            var bookToDelete = GetSingle(title);
            if (bookToDelete == null)
                throw new KeyNotFoundException("Database does not contain such book to delete.");

            _unitOfWork.GetRepository<Book>().Delete(bookToDelete);
            _unitOfWork.Save();
        }
    }
}