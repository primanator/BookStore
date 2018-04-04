namespace BLL.DTO
{
    using System;
    using System.Collections.Generic;

    public class BookDto : Dto
    {
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