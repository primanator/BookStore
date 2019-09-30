namespace Contracts.Models
{ 
    using System.Collections.Generic;

    public class LiteratureForm : BaseContract
    {
        public LiteratureForm()
        {
            Authors = new HashSet<Author>();
        }

        public ICollection<Author> Authors { get; set; }
    }
}