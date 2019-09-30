namespace Contracts.Models
{
    using System.Collections.Generic;

    public class Country : BaseContract
    {
        public Country()
        {
            Authors = new HashSet<Author>();
            Users = new HashSet<User>();
        }

        public ICollection<User> Users { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}