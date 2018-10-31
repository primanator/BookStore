namespace BLL.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using DAL.Interfaces;
    using DTO.Entities;
    using DTO.QueryBuilders;

    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookDtoFilterBuilder _filterBuilder;

        public BookService(IUnitOfWork unitOfWork, DtoFilterBuilder<BookDto> builder)
        {
            _unitOfWork = unitOfWork;
            _filterBuilder = new BookDtoFilterBuilder();
        }

        public void Create(BookDto record)
        {
            if (GetSingle(record.Name) != null)
                throw new ArgumentException("Database already contains book with such name.");

            _unitOfWork.GetRepository<BookDto>().Insert(record);
            _unitOfWork.Save();
        }

        public void Update(BookDto record)
        {
            var bookToUpdate = GetSingle(record.Name);
            if (bookToUpdate == null)
                throw new ArgumentException("Database does not contain such book to update.");

            bookToUpdate.SelfUpdate(record);

            _unitOfWork.GetRepository<BookDto>().Update(bookToUpdate);
            _unitOfWork.Save();
        }

        public IEnumerable<BookDto> GetAll()
        {
            var predicate = _filterBuilder.FindAll().Build();
            return _unitOfWork.GetRepository<BookDto>().FindBy(predicate);
        }

        public BookDto GetSingle(string title)
        {
            var predicate = _filterBuilder.FindByName(title).Build();
            return _unitOfWork.GetRepository<BookDto>().FindBy(predicate).SingleOrDefault();
        }

        public void Delete(string title)
        {
            var bookToDelete = GetSingle(title);
            if (bookToDelete == null)
                throw new KeyNotFoundException("Database does not contain such book to delete.");

            _unitOfWork.GetRepository<BookDto>().Delete(bookToDelete);
            _unitOfWork.Save();
        }
    }
}