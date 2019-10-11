namespace DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("LibraryId")]
        public virtual LibraryDto Library { get; set; }

        [ForeignKey("Id")]
        public virtual ICollection<AuthorDto> Authors { get; set; }

        [ForeignKey("Id")]
        public virtual ICollection<GenreDto> Genres { get; set; }
    }
}
