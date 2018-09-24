namespace DTO.Entities
{
    using System;
    using System.Collections.Generic;

    public class BookDto : EntityDto
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
        public LibraryDto Library { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }

        public ICollection<GenreDto> Genres { get; set; }
    }
}