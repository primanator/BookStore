namespace DTO.Entities
{
    using System.Collections.Generic;

    public class GenreDto : Dto
    {
        public GenreDto()
        {
            Books = new HashSet<BookDto>();
        }

        public ICollection<BookDto> Books { get; set; }
    }
}