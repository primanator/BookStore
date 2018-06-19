namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using DAL.Interfaces;
    using System.Linq;
    using DTO.Entities;

    public class BookStoreService : IBookStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookStoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateBook(Book record)
        {
            if (GetSingleBook(record.Name) != null)
                throw new ArgumentException("Database already contains book with such name.");

            _unitOfWork.GetBookRepository().Insert(record);
            _unitOfWork.Save();
        }

        public void UpdateBook(Book record)
        {
            var bookToUpdate = GetSingleBook(record.Name);

            if (bookToUpdate == null)
                throw new ArgumentException("Database does not contain such book to update.");

            var type = bookToUpdate.GetType();
            foreach (var property in type.GetProperties())
            {
                var newValue = type.GetProperty(property.Name)?.GetValue(record);
                property.SetValue(bookToUpdate, newValue);
            }

            _unitOfWork.GetBookRepository().Update(bookToUpdate);
            _unitOfWork.Save();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _unitOfWork.GetBookRepository().FindBy(book => true);
        }

        public Book GetSingleBook(string title)
        {
            return _unitOfWork.GetBookRepository().FindBy(b => b.Name == title).SingleOrDefault();
        }

        public void DeleteBook(string title)
        {
            Book bookToDelete = GetSingleBook(title);
            if (bookToDelete == null)
                throw new KeyNotFoundException("Database does not contain such book to delete.");

            _unitOfWork.GetBookRepository().Delete(bookToDelete);
            _unitOfWork.Save();
        }
    }
}