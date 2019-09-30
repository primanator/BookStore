namespace DAL.Interfaces
{
    using Contracts.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<TContract> 
        where TContract : BaseContract
    {
        List<TContract> FindBy(Expression<Func<TContract, bool>> dtoExpression);
        void Insert(TContract entity);
        void InsertMultiple(TContract[] entities);
        void Delete(TContract entity);
        void Update(TContract entity);
    }
}