using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime? WrittenIn { get; set; }

        /*public int LibraryId { get; set; }
        public LibraryDto Library { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }

        public ICollection<GenreDto> Genres { get; set; }*/
    }
}