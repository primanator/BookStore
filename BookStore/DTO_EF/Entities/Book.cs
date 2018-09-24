namespace DTO_EF.Entities
{
    using System;
    using System.Collections.Generic;

    public class Book : Entity
    {
        public Book()
        {
            Authors = new HashSet<Author>();
            Genres = new HashSet<Genre>();
        }

        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime WrittenIn { get; set; }

        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}
