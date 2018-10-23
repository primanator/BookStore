namespace DAL.Implementation
{
    using DTO.Entities;
    using DTO_EF.Entities;
    using EF;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class UnitOfWork : IUnitOfWork
    {
        protected readonly BookStoreContext context;

        public IRepository<BookDto> BookRepository { get; }

        private Dictionary<Type, Object> _repositoryDictionary { get; }

        private bool _disposed = false;

        public UnitOfWork(DbContext context)
        {
            this.context = context as BookStoreContext ?? throw new ArgumentException("Current Unit Of Work implementation is only confugured to work with " + typeof(BookStoreContext));

            _repositoryDictionary = new Dictionary<Type, Object>()
            {
                { typeof(BookDto), new Repository<Book, BookDto>(this.context) }
            };
        }

        public IRepository<T> GetRepository<T>()
            where T: Dto
        {
            var repositoryType = typeof(T);

            if (_repositoryDictionary.TryGetValue(repositoryType, out var repository))
                return (IRepository<T>)repository;

            return null;
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