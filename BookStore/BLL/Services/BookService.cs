namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using DAL.Interfaces;
    using System.Linq;
    using DTO.Entities;
    using AutoMapper;
    using DTO_EF.Entities;

    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(BookDto record)
        {
            if (GetSingle(record.Name) != null)
                throw new ArgumentException("Database already contains book with such name.");

            _unitOfWork.GetBookRepository().Insert(record);
            _unitOfWork.Save();
        }

        public void Update(BookDto record)
        {
            var bookToUpdate = GetSingle(record.Name);

            if (bookToUpdate == null)
                throw new ArgumentException("Database does not contain such book to update.");

            bookToUpdate.SelfUpdate<BookDto>(record);

            _unitOfWork.GetBookRepository().Update(bookToUpdate);
            _unitOfWork.Save();
        }

        public IEnumerable<BookDto> GetAll()
        {
            var mappedRepository = (IGenericRepository<Book>)Mapper.Map(_unitOfWork.GetBookRepository(), typeof(IGenericRepository<BookDto>), typeof(IGenericRepository<Book>));
            return Mapper.Map<IEnumerable<BookDto>>(mappedRepository.FindBy(book => true)); 
        }

        public BookDto GetSingle(string title)
        {
            return _unitOfWork.GetBookRepository().FindBy(b => b.Name == title).SingleOrDefault();
        }

        public void Delete(string title)
        {
            BookDto bookToDelete = GetSingle(title);
            if (bookToDelete == null)
                throw new KeyNotFoundException("Database does not contain such book to delete.");

            _unitOfWork.GetBookRepository().Delete(bookToDelete);
            _unitOfWork.Save();
        }
    }
}