namespace Contracts.QueryBuilders
{
    using Models;
    using Utils;

    public class BookFilterBuilder : ContractFilterBuilder<Book>
    {
        public BookFilterBuilder FindByIsbn(string isbn)
        {
            _toBuild = _toBuild.And(FuncToExpression(x => x.Isbn == isbn));
            return this;
        }

        public BookFilterBuilder LimitedEdition()
        {
            _toBuild = _toBuild.And(FuncToExpression(x => x.LimitedEdition == true));
            return this;
        }
    }
}