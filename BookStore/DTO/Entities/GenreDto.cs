namespace DTO.Entities
{
    using System.Collections.Generic;

    public class GenreDto : EntityDto
    {
        public GenreDto()
        {
            Books = new HashSet<BookDto>();
        }

        public ICollection<BookDto> Books { get; set; }
    }
}