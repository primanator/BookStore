namespace BLL.DTO
{
    using System.Collections.Generic;

    public class GenreDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BookDTO> Books { get; set; }
    }
}