namespace DTO_EF.Entities
{
    using System.Collections.Generic;

    public class LiteratureForm : Entity
    {
        public LiteratureForm()
        {
            Authors = new HashSet<Author>();
        }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
