namespace BLL.DTO
{
    using System;
    using System.Collections.Generic;

    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime? WrittenIn { get; set; }

        public int LibraryId { get; set; }
        public LibraryDTO Library { get; set; }

        public ICollection<AuthorDTO> Authors { get; set; }

        public ICollection<GenreDTO> Genres { get; set; }
    }
}