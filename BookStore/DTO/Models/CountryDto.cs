namespace DTO.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CountryDto : Dto
    {
        public CountryDto()
        {
            Authors = new HashSet<AuthorDto>();
            Users = new HashSet<UserDto>();
        }

        [ForeignKey("Id")]
        public virtual ICollection<UserDto> Users { get; set; }

        [ForeignKey("Id")]
        public virtual ICollection<AuthorDto> Authors { get; set; }
    }
}
