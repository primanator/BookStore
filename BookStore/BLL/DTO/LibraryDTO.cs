namespace BLL.DTO
{
    using System.Collections.Generic;

    public class LibraryDto : Dto
    {
        public ICollection<UserDto> Users { get; set; }

        public ICollection<BookDto> Books { get; set; }
    }
}