namespace DAL.Implementation
{
    using AutoMapper;
    using System;
    using EF;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Data.Entity;
    using Interfaces;
    using Contracts.Models;
    using DTO.Models;

    public class Repository<TDto, TContract> : IRepository<TContract>
        where TDto : Dto
        where TContract : BaseContract
    {
        public Repository(BookStoreContext context)
        {
            Db = context;
        }

        protected BookStoreContext Db { get; }

        public List<TContract> FindBy(Expression<Func<TContract, bool>> dtoExpression)
        {
            return
                Mapper.Map<IEnumerable<TContract>>(Db.Set<TDto>().AsNoTracking())
                   .Where(dtoExpression.Compile())
                    .ToList();
        }

        public void Insert(TContract entity)
        {
            Db.Set<TDto>().Add(Mapper.Map<TDto>(entity));
        }

        public void InsertMultiple(TContract[] entities)
        {
            Db.Set<TDto>().AddRange(Mapper.Map<TDto[]>(entities));
        }

        public void Update(TContract entity)
        {
            var mappedEntity = Mapper.Map<TDto>(entity);
            Db.Set<TDto>().Attach(mappedEntity);
            Db.Entry(mappedEntity).State = EntityState.Modified;
        }

        public void Delete(TContract entity)
        {
            var mappedEntity = Mapper.Map<TDto>(entity);
            Db.Entry(mappedEntity).State = EntityState.Deleted;
        }
    }
}