namespace DTO.Models
{
    using System.Collections.Generic;

    public class GenreDto : Dto
    {
        public GenreDto()
        {
            Books = new HashSet<BookDto>();
        }

        public virtual ICollection<BookDto> Books { get; set; }
    }
}
