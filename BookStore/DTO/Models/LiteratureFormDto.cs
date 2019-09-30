namespace DTO.Models
{
    using System.Collections.Generic;

    public class LiteratureFormDto : Dto
    {
        public LiteratureFormDto()
        {
            Authors = new HashSet<AuthorDto>();
        }

        public virtual ICollection<AuthorDto> Authors { get; set; }
    }
}
