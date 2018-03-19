namespace BLL.DTO
{
    using System.Collections.Generic;

    public class LiteratureFormDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AuthorDTO> Authors { get; set; }
    }
}