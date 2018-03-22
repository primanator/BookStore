namespace BLL.DTO
{
    using System.Collections.Generic;

    public class CountryDto : Dto
    {
        public ICollection<UserDto> Users { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }
    }
}