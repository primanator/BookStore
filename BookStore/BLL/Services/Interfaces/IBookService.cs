namespace BLL.Services.Interfaces
{
    using Contracts.Models;
    using System.Collections.Generic;

    public interface IBookService
    {
        void Create(Book record);

        void Update(Book record);

        IEnumerable<Book> GetAll();

        Book GetSingle(string title);

        void Delete(string title);
    }
}