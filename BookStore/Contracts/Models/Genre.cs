namespace Contracts.Models
{
    using System.Collections.Generic;

    public class Genre : BaseContract
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public ICollection<Book> Books { get; set; }
    }
}