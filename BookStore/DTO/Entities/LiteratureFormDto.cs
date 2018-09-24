namespace DTO.Entities
{ 
    using System.Collections.Generic;

    public class LiteratureFormDto : EntityDto
    {
        public LiteratureFormDto()
        {
            Authors = new HashSet<AuthorDto>();
        }

        public ICollection<AuthorDto> Authors { get; set; }
    }
}