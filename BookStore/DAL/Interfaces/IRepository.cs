namespace DAL.Interfaces
{
    using DTO.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<TDto> 
        where TDto : Dto
    {
        List<TDto> FindBy(Expression<Func<TDto, bool>> dtoExpression);
        void Insert(TDto entity);
        void InsertMultiple(TDto[] entities);
        void Delete(TDto entity);
        void Update(TDto entity);
    }
}