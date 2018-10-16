namespace DTO.Entities
{
    public class UserDto : EntityDto
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int Age { get; set; }
        public int CountryId { get; set; }
        public CountryDto Country { get; set; } 
        public int LibraryId { get; set; }
        public LibraryDto Library { get; set; }
    }
}