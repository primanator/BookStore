namespace DTO.QueryBuilder
{
    using System;
    using System.Linq.Expressions;
    using DTO.Entities;

    public class BookDtoFilterBuilder : DtoFilterBuilder<Book>
    {
        public BookDtoFilterBuilder FindByIsbn(string isbn)
        {
            Expression.Add(_toBuild, FuncToExpression(x => x.Isbn == isbn));

            return this;
        }

        public BookDtoFilterBuilder FindTopReaded()
        {
            Expression.Add(_toBuild, FuncToExpression(x => x.WrittenIn >= DateTime.MinValue && x.LimitedEdition));

            return this;

        }
    }
}