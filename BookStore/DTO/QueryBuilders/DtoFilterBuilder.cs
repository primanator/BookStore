namespace DTO.QueryBuilder
{
    using System;
    using System.Linq.Expressions;
    using DTO.Entities;

    public class DtoFilterBuilder<T> where T: Entity
    {
        protected Expression<Func<T, bool>> _toBuild;

        public DtoFilterBuilder() { }

        public DtoFilterBuilder<T> FindByName(string name)
        {
            Expression.Add(_toBuild, FuncToExpression(x => x.Name == name));

            return this;
        }

        public DtoFilterBuilder<T> FindById(int id)
        {
            Expression.Add(_toBuild, FuncToExpression(x => x.Id == id));

            return this;
        }

        public Expression<Func<T, bool>> Build()
        {
            return _toBuild;
        }

        protected static Expression<Func<T, bool>> FuncToExpression(Func<T, bool> f)
        {
            return x => f(x);
        }
    }
}