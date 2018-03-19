namespace BLL.DTO
{
    using System.Collections.Generic;

    public class LibraryDTO
    {
        public int Id { get; set; }

        public ICollection<UserDTO> Users { get; set; }

        public ICollection<BookDTO> Books { get; set; }
    }
}