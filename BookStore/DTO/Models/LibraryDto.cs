namespace DTO.Models
{
    using System.Collections.Generic;

    public class LibraryDto : Dto
    {
        public LibraryDto()
        {
            Books = new HashSet<BookDto>();
            Users = new HashSet<UserDto>();
        }

        public virtual ICollection<UserDto> Users { get; set; }

        public virtual ICollection<BookDto> Books { get; set; }
    }
}
