namespace DTO.QueryBuilders
{
    using System;
    using System.Linq.Expressions;
    using Utils;
    using Entities;

    public class DtoFilterBuilder<T> where T : EntityDto
    {
        protected Expression<Func<T, bool>> _toBuild;

        public DtoFilterBuilder()
        {
            _toBuild = FuncToExpression(x => true);
        }

        public DtoFilterBuilder<T> FindByName(string name)
        {
            _toBuild = _toBuild.And(FuncToExpression(x => x.Name == name));
            return this;
        }

        public DtoFilterBuilder<T> FindById(int id)
        {
            _toBuild = _toBuild.And(FuncToExpression(x => x.Id == id));
            return this;
        }

        public DtoFilterBuilder<T> FindAll()
        {
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