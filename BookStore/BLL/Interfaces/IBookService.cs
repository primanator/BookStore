namespace BLL.Interfaces
{
    using System.Collections.Generic;
    using DTO.Entities;

    public interface IBookService
    {
        void Create(Book record);

        void Update(Book record);

        IEnumerable<Book> GetAll();

        Book GetSingle(string title);

        void Delete(string title);
    }
}