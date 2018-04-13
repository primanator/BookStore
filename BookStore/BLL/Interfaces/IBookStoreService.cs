namespace BLL.Interfaces
{
    using System.Collections.Generic;
    using System;
    using DTO;

    public interface IBookStoreService
    {
        void CreateBook(BookDto record);

        void UpdateBook(BookDto record);

        IEnumerable<BookDto> GetAllBooks();

        BookDto GetSingleBook(string title);

        void DeleteBook(string title);
    }
}