namespace BLL.DTO
{
    using System;
    using System.Collections.Generic;

    public class AuthorDto : Dto
    {
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public int CountryId { get; set; }
        public CountryDto Country { get; set; }

        public ICollection<LiteratureFormDto> LiteratureForms { get; set; }

        public ICollection<BookDto> Books { get; set; }
    }
}