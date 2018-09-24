namespace DTO.Entities
{
    using System.Collections.Generic;

    public class CountryDto : EntityDto
    {
        public CountryDto()
        {
            Authors = new HashSet<AuthorDto>();
            Users = new HashSet<UserDto>();
        }

        public ICollection<UserDto> Users { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }
    }
}