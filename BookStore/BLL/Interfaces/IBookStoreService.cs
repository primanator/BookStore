namespace BLL.Interfaces
{
    using System.Collections.Generic;
    using DTO.Entities;

    public interface IBookStoreService
    {
        void CreateBook(Book record);

        void UpdateBook(Book record);

        IEnumerable<Book> GetAllBooks();

        Book GetSingleBook(string title);

        void DeleteBook(string title);
    }
}