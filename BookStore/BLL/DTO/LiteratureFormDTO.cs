namespace BLL.DTO
{
    using System.Collections.Generic;

    public class LiteratureFormDto : Dto
    {
        public ICollection<AuthorDto> Authors { get; set; }
    }
}