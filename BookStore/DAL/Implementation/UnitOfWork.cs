namespace DAL.Implementation
{
    using AutoMapper;
    using EF;
    using Interfaces;
    using DTO.Entities;
    using DTO_EF.Entities;
    using System;
    using System.Data.Entity;

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

        private bool _disposed = false;

        public UnitOfWork(DbContext context)
        {
            if (!(context is BookStoreContext))
                throw new ArgumentException("Current Unit Of Work implementation is only confugured to work with " + typeof(BookStoreContext));
            this.context = (BookStoreContext)context;
        }

        public IGenericRepository<AuthorDto> GetAuthorRepository()
        {
            return Mapper.Map<IGenericRepository<AuthorDto>>(_authorRepository ?? (_authorRepository = new GenericRepository<Author>(context)));
        }

        public IGenericRepository<BookDto> GetBookRepository()
        {
            return Mapper.Map<IGenericRepository<BookDto>>(_bookRepository ?? (_bookRepository = new GenericRepository<Book>(context)));
        }

        public IGenericRepository<CountryDto> GetCountryRepository()
        {
            return Mapper.Map<IGenericRepository<CountryDto>>(_countryRepository ?? (_countryRepository = new GenericRepository<Country>(context)));
        }

        public IGenericRepository<GenreDto> GetGenreRepository()
        {
            return Mapper.Map<IGenericRepository<GenreDto>>(_genreRepository ?? (_genreRepository = new GenericRepository<Genre>(context)));
        }

        public IGenericRepository<LibraryDto> GetLibraryRepository()
        {
            return Mapper.Map<IGenericRepository<LibraryDto>>(_libraryRepository ?? (_libraryRepository = new GenericRepository<Library>(context)));
        }

        public IGenericRepository<LiteratureFormDto> GetLiteratureFormRepository()
        {
            return Mapper.Map<IGenericRepository<LiteratureFormDto>>(_literatureFormRepository ?? (_literatureFormRepository = new GenericRepository<LiteratureForm>(context)));
        }

        public IGenericRepository<UserDto> GetUserRepository()
        {
            return Mapper.Map<IGenericRepository<UserDto>>(_userRepository ?? (_userRepository = new GenericRepository<User>(context)));
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}