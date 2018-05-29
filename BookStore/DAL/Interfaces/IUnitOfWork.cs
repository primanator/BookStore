namespace DAL.Interfaces
{
    using Entities;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Author> GetAuthorRepository();
        IGenericRepository<Book> GetBookRepository();
        IGenericRepository<Country> GetCountryRepository();
        IGenericRepository<Genre> GetGenreRepository();
        IGenericRepository<Library> GetLibraryRepository();
        IGenericRepository<LiteratureForm> GetLiteratureFormRepository();
        IGenericRepository<User> GetUserRepository();
        void Save();
    }
}