namespace API.Models
{
    using System;
    using System.Collections.Generic;

    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime WrittenIn { get; set; }

        public int LibraryId { get; set; }
        /*public LibraryModel Library { get; set; }

        public ICollection<AuthorModel> Authors { get; set; }

        public ICollection<GenreModel> Genres { get; set; }*/
    }
}