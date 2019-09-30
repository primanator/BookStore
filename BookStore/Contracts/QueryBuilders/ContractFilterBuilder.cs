namespace Contracts.QueryBuilders
{
    using Contracts.Models;
    using System;
    using System.Linq.Expressions;
    using Utils;

    public abstract class ContractFilterBuilder<T> where T : BaseContract
    {
        protected Expression<Func<T, bool>> _toBuild;

        public ContractFilterBuilder()
        {
            _toBuild = FuncToExpression(x => true);
        }

        public ContractFilterBuilder<T> FindByName(string name)
        {
            _toBuild = _toBuild.And(FuncToExpression(x => x.Name == name));
            return this;
        }

        public ContractFilterBuilder<T> FindById(int id)
        {
            _toBuild = _toBuild.And(FuncToExpression(x => x.Id == id));
            return this;
        }

        public ContractFilterBuilder<T> FindAll()
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