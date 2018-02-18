﻿namespace DAL.Entities
{
    using System.Collections.Generic;

    public class Country : Entity
    {
        public Country()
        {
            Authors = new HashSet<Author>();
            Users = new HashSet<User>();
        }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}