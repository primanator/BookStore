namespace DAL.Interfaces
{
    using DTO.Entities;
    using DTO_EF.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T, T1> 
        where T : Entity
        where T1: EntityDto
    {
        List<T1> FindBy(Expression<Func<T1, bool>> dtoExpression);
        void Insert(T1 entity);
        void Delete(T1 entity);
        void Update(T1 entity);
        T1 Get(int id);
    }
}