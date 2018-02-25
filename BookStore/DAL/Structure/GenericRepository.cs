namespace DAL.Structure
{
    using System;
    using DAL.Entities;
    using DAL.Interfaces;
    using DAL.EF;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Data.Entity;

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly BookStoreContext _db;

        protected GenericRepository(BookStoreContext context)
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

            T entity = GetEntity(id);

            if (entity == null)
                throw new InvalidOperationException(string.Format("{0} with Id={1} was not found in the DB", typeof(T).Name, id));

            return entity;
        }

        protected virtual T GetEntity(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public void Save(T entity)
        {
            if (entity.Id == 0)
                Insert(entity);
            else
                Update(entity);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (_db.Set<T>().Find(entity.Id) != null)
                _db.Entry(entity).State = EntityState.Detached;
            _db.SaveChanges();
        }
    }
}