namespace DTO.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class UserDto : Dto
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int Age { get; set; }

        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual CountryDto Country { get; set; }

        public int LibraryId { get; set; }
        [ForeignKey("LibraryId")]
        public virtual LibraryDto Library { get; set; }
    }
}
