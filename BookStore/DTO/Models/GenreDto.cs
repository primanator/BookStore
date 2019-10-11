namespace DTO.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class GenreDto : Dto
    {
        public GenreDto()
        {
            Books = new HashSet<BookDto>();
        }

        [ForeignKey("Id")]
        public virtual ICollection<BookDto> Books { get; set; }
    }
}
