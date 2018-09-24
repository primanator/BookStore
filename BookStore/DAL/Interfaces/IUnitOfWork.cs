namespace DAL.Interfaces
{
    using DTO.Entities;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<AuthorDto> GetAuthorRepository();
        IGenericRepository<BookDto> GetBookRepository();
        IGenericRepository<CountryDto> GetCountryRepository();
        IGenericRepository<GenreDto> GetGenreRepository();
        IGenericRepository<LibraryDto> GetLibraryRepository();
        IGenericRepository<LiteratureFormDto> GetLiteratureFormRepository();
        IGenericRepository<UserDto> GetUserRepository();
        void Save();
    }
}