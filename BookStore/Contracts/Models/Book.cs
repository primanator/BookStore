namespace Contracts.Models
{
    using System;
    using System.Collections.Generic;

    public class Book : BaseContract
    {
        public Book()
        {
            Authors = new HashSet<Author>();
            Genres = new HashSet<Genre>();
            LibraryId = 1; // only one library is supported for now
        }

        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime WrittenIn { get; set; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }

        public ICollection<Author> Authors { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}