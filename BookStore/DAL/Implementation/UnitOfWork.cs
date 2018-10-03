namespace DAL.Implementation
{
    using DTO.Entities;
    using EF;
    using DTO_EF.Entities;
    using Interfaces;
    using System;
    using System.Data.Entity;

    public class UnitOfWork : IUnitOfWork
    {
        protected readonly BookStoreContext context;

        public IRepository<Book, BookDto> BookRepository { get; }

        private bool _disposed = false;

        public UnitOfWork(DbContext context)
        {
            if (!(context is BookStoreContext))
                throw new ArgumentException("Current Unit Of Work implementation is only confugured to work with " + typeof(BookStoreContext));
            this.context = (BookStoreContext)context;

            BookRepository = new Repository<Book, BookDto>(this.context);
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