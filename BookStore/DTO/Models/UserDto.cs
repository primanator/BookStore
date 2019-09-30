namespace DTO.Models
{
    public class UserDto : Dto
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int Age { get; set; }

        public int CountryId { get; set; }
        public virtual CountryDto Country { get; set; }

        public int LibraryId { get; set; }
        public virtual LibraryDto Library { get; set; }
    }
}
