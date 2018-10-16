namespace DTO.QueryBuilders
{
    using Entities;
    using Utils;

    public class BookDtoFilterBuilder : DtoFilterBuilder<BookDto>
    {
        public BookDtoFilterBuilder FindByIsbn(string isbn)
        {
            _toBuild = _toBuild.And(FuncToExpression(x => x.Isbn == isbn));
            return this;
        }

        public BookDtoFilterBuilder LimitedEdition()
        {
            _toBuild = _toBuild.And(FuncToExpression(x => x.LimitedEdition == true));
            return this;
        }
    }
}