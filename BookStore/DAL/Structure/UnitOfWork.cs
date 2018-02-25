﻿namespace DAL.Structure
{
    using DAL.Interfaces;
    using DAL.Entities;
    using DAL.EF;

    public class UnitOfWork : IUnitOfWork
    {
        protected readonly BookStoreContext context;

        private IGenericRepository<Author> _authorRepository;
        private IGenericRepository<Book> _bookRepository;
        private IGenericRepository<Country> _countryRepository;
        private IGenericRepository<Genre> _genreRepository;
        private IGenericRepository<Library> _libraryRepository;
        private IGenericRepository<LiteratureForm> _literatureFormRepository;
        private IGenericRepository<User> _userRepository;

        public UnitOfWork(string connection)
        {
            context = new BookStoreContext(connection);
        }

        public IGenericRepository<Author> GetAuthorRepository()
        {
            return _authorRepository ?? (_authorRepository = new GenericRepository<Author>(context));
        }

        public IGenericRepository<Book> GetBookRepository()
        {
            return _bookRepository ?? (_bookRepository = new GenericRepository<Book>(context));
        }

        public IGenericRepository<Country> GetCountryRepository()
        {
            return _countryRepository ?? (_countryRepository = new GenericRepository<Country>(context));
        }

        public IGenericRepository<Genre> GetGenreRepository()
        {
            return _genreRepository ?? (_genreRepository = new GenericRepository<Genre>(context));
        }

        public IGenericRepository<Library> GetLibraryRepository()
        {
            return _libraryRepository ?? (_libraryRepository = new GenericRepository<Library>(context));
        }

        public IGenericRepository<LiteratureForm> GetLiteratureFormRepository()
        {
            return _literatureFormRepository ?? (_literatureFormRepository = new GenericRepository<LiteratureForm>(context));
        }

        public IGenericRepository<User> GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new GenericRepository<User>(context));
        }
    }
}