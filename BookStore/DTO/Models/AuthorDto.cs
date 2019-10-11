namespace DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AuthorDto : Dto
    {
        public AuthorDto()
        {
            Books = new HashSet<BookDto>();
            LiteratureForms = new HashSet<LiteratureFormDto>();
        }

        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual CountryDto Country { get; set; }

        [ForeignKey("Id")]
        public virtual ICollection<LiteratureFormDto> LiteratureForms { get; set; }

        [ForeignKey("Id")]
        public virtual ICollection<BookDto> Books { get; set; }
    }
}
