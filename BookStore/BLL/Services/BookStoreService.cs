namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using Interfaces;
    using AutoMapper;
    using DAL.Interfaces.UnitOfWork;
    using DAL.Entities;
    using System.Linq;

    public class BookStoreService : IBookStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookStoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateBook(BookDto record)
        {
            if (GetSingleBook(record.Name) != null)
                throw new ArgumentException("Database already contains book with such name.");

            _unitOfWork.GetBookRepository().Insert(Mapper.Map<Book>(record));
        }

        public void UpdateBook(BookDto record)
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

            _unitOfWork.GetBookRepository().Update(Mapper.Map<Book>(bookToUpdate));
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            return Mapper.Map<IEnumerable<BookDto>>(_unitOfWork.GetBookRepository().FindBy(b => true));
        }

        public BookDto GetSingleBook(string title)
        {
            return Mapper.Map<BookDto>(_unitOfWork.GetBookRepository().FindBy(b => b.Name == title).SingleOrDefault());
        }

        public void DeleteBook(string title)
        {
            BookDto bookToDelete = GetSingleBook(title);
            if (bookToDelete == null)
                throw new KeyNotFoundException("Database does not contain such book to delete.");

            _unitOfWork.GetBookRepository().Delete(Mapper.Map<Book>(bookToDelete));
        }
    }
}