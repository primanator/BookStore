namespace DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save(T entity);
        T Get(int id);
    }
}