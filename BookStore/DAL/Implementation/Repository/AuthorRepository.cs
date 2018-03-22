namespace DAL.Implementation.Repository
{
    using System.Collections.Generic;
    using EF;
    using Entities;
    using Interfaces.Repository;
    using System;
    using System.Linq;

    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookStoreContext context) : base(context) { }

        public IEnumerable<Author> Get21CenturyAuthors()
        {
            return Db.Authors.Where(a => a.DeathDate < DateTime.Today);
        }
    }
}