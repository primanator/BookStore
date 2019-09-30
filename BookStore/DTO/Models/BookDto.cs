namespace DTO.Models
{
    using System;
    using System.Collections.Generic;

    public class BookDto : Dto
    {
        public BookDto()
        {
            Authors = new HashSet<AuthorDto>();
            Genres = new HashSet<GenreDto>();
        }

        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime WrittenIn { get; set; }

        public int LibraryId { get; set; }
        public virtual LibraryDto Library { get; set; }

        public virtual ICollection<AuthorDto> Authors { get; set; }

        public virtual ICollection<GenreDto> Genres { get; set; }
    }
}
