namespace DAL.Interfaces
{
    using DTO.Entities;
    using DTO_EF.Entities;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book, BookDto> BookRepository { get; }
        void Save();
    }
}