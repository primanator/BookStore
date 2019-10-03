namespace DTO.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Genre")]
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
