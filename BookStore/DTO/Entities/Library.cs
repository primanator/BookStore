namespace DTO.Entities
{
    using System.Collections.Generic;

    public class Library : Entity
    {
        public Library()
        {
            Books = new HashSet<Book>();
            Users = new HashSet<User>();
        }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}