namespace DAL.Interfaces
{
    using DAL.Entities;

    public interface IUnitOfWork
    {
        IGenericRepository<Author> GetAuthorRepository();
        IGenericRepository<Book> GetBookRepository();
        IGenericRepository<Country> GetCountryRepository();
        IGenericRepository<Genre> GetGenreRepository();
        IGenericRepository<Library> GetLibraryRepository();
        IGenericRepository<LiteratureForm> GetLiteratureFormRepository();
        IGenericRepository<User> GetUserRepository();
    }
}