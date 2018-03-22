namespace DAL.Interfaces.UnitOfWork
{
    using Repository;

    public interface IUnitOfWork
    {
        IAuthorRepository GetAuthorRepository();
        IBookRepository GetBookRepository();
        ICountryRepository GetCountryRepository();
        IGenreRepository GetGenreRepository();
        ILibraryRepository GetLibraryRepository();
        ILiteratureFormRepository GetLiteratureFormRepository();
        IUserRepository GetUserRepository();
    }
}