namespace Contracts.Models
{
    using System.Collections.Generic;

    public class Library : BaseContract
    {
        public Library()
        {
            Books = new HashSet<Book>();
            Users = new HashSet<User>();
        }

        public ICollection<User> Users { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}