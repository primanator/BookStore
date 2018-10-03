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

    public class Repository<T, T1> : IRepository<T, T1>
        where T : Entity
        where T1 : EntityDto
    {
        public Repository(BookStoreContext context)
        {
            Db = context;
        }

        protected BookStoreContext Db { get; }

        public List<T1> FindBy(Expression<Func<T1, bool>> dtoExpression)
        {
            return
                Mapper.Map<IEnumerable<T1>>(Db.Set<T>().AsNoTracking())
                   .Where(dtoExpression.Compile())
                    .ToList();
        }

        public T1 Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Entity Id is less or equals zero.");

            var entityToMap = Db.Set<T>().Find(id);
            if (entityToMap == null)
                throw new InvalidOperationException(string.Format("{0} with Id={1} was not found in the DB", typeof(T1).Name, id));

            return Mapper.Map<T1>(entityToMap);
        }

        public void Insert(T1 entity)
        {
            Db.Set<T>().Add(Mapper.Map<T>(entity));
        }

        public void Update(T1 entity)
        {
            var mappedEntity = Mapper.Map<T>(entity);
            Db.Set<T>().Attach(mappedEntity);
            Db.Entry(mappedEntity).State = EntityState.Modified;
        }

        public void Delete(T1 entity)
        {
            var mappedEntity = Mapper.Map<T>(entity);
            Db.Entry(mappedEntity).State = EntityState.Deleted;
        }
    }
}