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
        protected readonly BookStoreContext Context;

        private Dictionary<Type, Object> _repositoryDictionary { get; }

        private bool _disposed = false;

        public UnitOfWork(DbContext context)
        {
            Context = context as BookStoreContext ?? throw new ArgumentException("Current Unit Of Work implementation is only confugured to work with " + typeof(BookStoreContext));
            _repositoryDictionary = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>()
            where T: Dto
        {
            var repositoryType = typeof(T);

            if (_repositoryDictionary.TryGetValue(repositoryType, out var repository))
                return (IRepository<T>)repository;

            repository = new Repository<Book, BookDto>(Context);
            _repositoryDictionary.Add(repositoryType, repository);

            return (IRepository<T>)repository;
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