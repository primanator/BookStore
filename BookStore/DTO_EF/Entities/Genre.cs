namespace DTO_EF.Entities
{
    using System.Collections.Generic;

    public class Genre : Entity
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public virtual ICollection<Book> Books { get; set; }
    }
}
