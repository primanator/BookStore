namespace BLL.DTO
{
    using System.Collections.Generic;

    public class GenreDto : Dto
    {
        public ICollection<BookDto> Books { get; set; }
    }
}