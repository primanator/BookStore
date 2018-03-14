namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;

    public class Author : Entity
    {
        public Author()
        {
            Books = new HashSet<Book>();
            LiteratureForms = new HashSet<LiteratureForm>();
        }

        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public virtual ICollection<LiteratureForm> LiteratureForms { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}