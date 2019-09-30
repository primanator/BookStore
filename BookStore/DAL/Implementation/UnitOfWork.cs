namespace DAL.Implementation
{
    using Contracts.Models;
    using DTO.Models;
    using EF;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class UnitOfWork : IUnitOfWork
    {
        protected readonly BookStoreContext Context;

        private Dictionary<Type, Object> _repositoryDictionary { get; }

        private bool _disposed = false;

        public UnitOfWork(DbContext context)
        {
            Context = context as BookStoreContext ?? throw new ArgumentException("Current Unit Of Work implementation is only confugured to work with " + typeof(BookStoreContext));

            _repositoryDictionary = new Dictionary<Type, Object>()
            {
                { typeof(Book), new Repository<BookDto, Book>(Context) },
                { typeof(Library), new Repository<LibraryDto, Library>(Context) },
                { typeof(Author), new Repository<AuthorDto, Author>(Context) },
                { typeof(Genre), new Repository<GenreDto, Genre>(Context) }
            };
        }

        public IRepository<T> GetRepository<T>()
            where T: BaseContract
        {
            if (_repositoryDictionary.TryGetValue(typeof(T), out var repository))
                return (IRepository<T>)repository;

            return null;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
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