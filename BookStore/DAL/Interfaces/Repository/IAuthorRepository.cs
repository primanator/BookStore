namespace DAL.Interfaces.Repository
{
    using Entities;
    using System.Collections.Generic;

    public interface IAuthorRepository : IGenericRepository<Author>
    {
        IEnumerable<Author> Get21CenturyAuthors();
    }
}