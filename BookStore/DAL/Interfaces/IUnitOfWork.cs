namespace DAL.Interfaces
{
    using DTO.Entities;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<BookDto> BookRepository { get; }

        IRepository<T> GetRepository<T>() where T : Dto;

        void Save();
    }
}