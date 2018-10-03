namespace DTO.QueryBuilders
{
    using System;
    using System.Linq.Expressions;
    using Entities;

    public class BookDtoFilterBuilder : DtoFilterBuilder<BookDto>
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