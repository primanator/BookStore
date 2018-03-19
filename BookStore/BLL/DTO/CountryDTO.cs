namespace BLL.DTO
{
    using System.Collections.Generic;

    public class CountryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserDTO> Users { get; set; }

        public ICollection<AuthorDTO> Authors { get; set; }
    }
}