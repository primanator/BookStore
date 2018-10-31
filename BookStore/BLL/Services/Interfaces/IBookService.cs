namespace BLL.Services.Interfaces
{
    using System.Collections.Generic;
    using DTO.Entities;

    public interface IBookService
    {
        void Create(BookDto record);

        void Update(BookDto record);

        IEnumerable<BookDto> GetAll();

        BookDto GetSingle(string title);

        void Delete(string title);
    }
}