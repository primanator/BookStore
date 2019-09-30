namespace DTO.Models
{
    using System.Collections.Generic;

    public class CountryDto : Dto
    {
        public CountryDto()
        {
            Authors = new HashSet<AuthorDto>();
            Users = new HashSet<UserDto>();
        }

        public virtual ICollection<UserDto> Users { get; set; }

        public virtual ICollection<AuthorDto> Authors { get; set; }
    }
}
