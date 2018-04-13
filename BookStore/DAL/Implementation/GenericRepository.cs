namespace DAL.Implementation
{
    using System;
    using Entities;
    using EF;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Data.Entity;
    using DAL.Interfaces;

    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly BookStoreContext _db;

        public GenericRepository(BookStoreContext context)
        {
            _db = context;
        }

        protected BookStoreContext Db
        {
            get { return _db; }
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().AsNoTracking().Where(predicate).ToList();
        }

        public T Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Entity Id is less or equals zero.");

            T entity = _db.Set<T>().Find(id);

            if (entity == null)
                throw new InvalidOperationException(string.Format("{0} with Id={1} was not found in the DB", typeof(T).Name, id));

            return entity;
        }

        public void Insert(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _db.Entry(_db.Set<T>().Find(entity.Id)).State = EntityState.Deleted;
        }
    }
}