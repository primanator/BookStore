namespace DTO.Entities
{
    using System.Collections.Generic;

    public class LibraryDto : EntityDto
    {
        public LibraryDto()
        {
            Books = new HashSet<BookDto>();
            Users = new HashSet<UserDto>();
        }

        public ICollection<UserDto> Users { get; set; }
        public ICollection<BookDto> Books { get; set; }
    }
}