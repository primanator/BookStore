namespace Contracts.Models
{
    using System;
    using System.Collections.Generic;

    public class Author : BaseContract
    {
        public Author()
        {
            Books = new HashSet<Book>();
            LiteratureForms = new HashSet<LiteratureForm>();
        }

        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<LiteratureForm> LiteratureForms { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}