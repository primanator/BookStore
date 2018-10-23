namespace DAL.Implementation
{
    using AutoMapper;
    using DTO.Entities;
    using DTO_EF.Entities;
    using System;
    using EF;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Data.Entity;
    using Interfaces;

    public class Repository<TEntity, TDto> : IRepository<TDto>
        where TEntity : Entity
        where TDto : Dto
    {
        public Repository(BookStoreContext context)
        {
            Db = context;
        }

        protected BookStoreContext Db { get; }

        public List<TDto> FindBy(Expression<Func<TDto, bool>> dtoExpression)
        {
            return
                Mapper.Map<IEnumerable<TDto>>(Db.Set<TEntity>().AsNoTracking())
                   .Where(dtoExpression.Compile())
                    .ToList();
        }

        public void Insert(TDto entity)
        {
            Db.Set<TEntity>().Add(Mapper.Map<TEntity>(entity));
        }

        public void Update(TDto entity)
        {
            var mappedEntity = Mapper.Map<TEntity>(entity);
            Db.Set<TEntity>().Attach(mappedEntity);
            Db.Entry(mappedEntity).State = EntityState.Modified;
        }

        public void Delete(TDto entity)
        {
            var mappedEntity = Mapper.Map<TEntity>(entity);
            Db.Entry(mappedEntity).State = EntityState.Deleted;
        }
    }
}