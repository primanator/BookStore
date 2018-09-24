namespace DTO_EF.Entities
{
    using System.Collections.Generic;

    public class Country : Entity
    {
        public Country()
        {
            Authors = new HashSet<Author>();
            Users = new HashSet<User>();
        }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
