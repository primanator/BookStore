﻿namespace DTO.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LiteratureFormDto : Dto
    {
        public LiteratureFormDto()
        {
            Authors = new HashSet<AuthorDto>();
        }

        [ForeignKey("Id")]
        public virtual ICollection<AuthorDto> Authors { get; set; }
    }
}
