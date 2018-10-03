﻿using DTO_EF.Entities;

namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using DAL.Interfaces;
    using System.Linq;
    using DTO.Entities;
    using DTO.QueryBuilders;

    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookDtoFilterBuilder _filter;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _filter = new BookDtoFilterBuilder();
        }

        public void Create(BookDto record)
        {
            if (GetSingle(record.Name) != null)
                throw new ArgumentException("Database already contains book with such name.");

            _unitOfWork.BookRepository.Insert(record);
            _unitOfWork.Save();
        }

        public void Update(BookDto record)
        {
            var bookToUpdate = GetSingle(record.Name);

            if (bookToUpdate == null)
                throw new ArgumentException("Database does not contain such book to update.");

            bookToUpdate.SelfUpdate(record);

            _unitOfWork.BookRepository.Update(bookToUpdate);
            _unitOfWork.Save();
        }

        public IEnumerable<BookDto> GetAll()
        {
            var predicate = _filter.FindAll().Build();
            return _unitOfWork.BookRepository.FindBy(predicate);
        }

        public BookDto GetSingle(string title)
        {
            return _unitOfWork.BookRepository.FindBy(x => x.Name == title).SingleOrDefault();
        }

        public void Delete(string title)
        {
            var bookToDelete = GetSingle(title);
            if (bookToDelete == null)
                throw new KeyNotFoundException("Database does not contain such book to delete.");

            _unitOfWork.BookRepository.Delete(bookToDelete);
            _unitOfWork.Save();
        }
    }
}