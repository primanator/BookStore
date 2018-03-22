namespace DAL.Implementation.UnitOfWork
{
    using EF;
    using Repository;
    using Interfaces.Repository;
    using Interfaces.UnitOfWork;

    public class UnitOfWork : IUnitOfWork
    {
        protected readonly BookStoreContext context;

        private IAuthorRepository _authorRepository;
        private IBookRepository _bookRepository;
        private ICountryRepository _countryRepository;
        private IGenreRepository _genreRepository;
        private ILibraryRepository _libraryRepository;
        private ILiteratureFormRepository _literatureFormRepository;
        private IUserRepository _userRepository;

        public UnitOfWork()
        {
            context = new BookStoreContext();
        }

        public IAuthorRepository GetAuthorRepository()
        {
            return _authorRepository ?? (_authorRepository = new AuthorRepository(context));
        }

        public IBookRepository GetBookRepository()
        {
            return _bookRepository ?? (_bookRepository = new BookRepository(context));
        }

        public ICountryRepository GetCountryRepository()
        {
            return _countryRepository ?? (_countryRepository = new CountryRepository(context));
        }

        public IGenreRepository GetGenreRepository()
        {
            return _genreRepository ?? (_genreRepository = new GenreRepository(context));
        }

        public ILibraryRepository GetLibraryRepository()
        {
            return _libraryRepository ?? (_libraryRepository = new LibraryRepository(context));
        }

        public ILiteratureFormRepository GetLiteratureFormRepository()
        {
            return _literatureFormRepository ?? (_literatureFormRepository = new LiteratureFormRepository(context));
        }

        public IUserRepository GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository(context));
        }
    }
}