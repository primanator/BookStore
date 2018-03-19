namespace BLL.DTO
{
    using System;
    using System.Collections.Generic;

    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public int CountryId { get; set; }
        public CountryDTO Country { get; set; }

        public ICollection<LiteratureFormDTO> LiteratureForms { get; set; }

        public ICollection<BookDTO> Books { get; set; }
    }
}