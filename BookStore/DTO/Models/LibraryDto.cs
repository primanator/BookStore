namespace DTO.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Library")]
    public class LibraryDto : Dto
    {
        public LibraryDto()
        {
            Books = new HashSet<BookDto>();
            Users = new HashSet<UserDto>();
        }

        [ForeignKey("Id")]
        public virtual ICollection<UserDto> Users { get; set; }

        [ForeignKey("Id")]
        public virtual ICollection<BookDto> Books { get; set; }
    }
}
