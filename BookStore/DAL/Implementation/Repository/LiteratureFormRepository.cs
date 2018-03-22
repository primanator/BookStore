namespace DAL.Implementation.Repository
{
    using EF;
    using Entities;
    using Interfaces.Repository;

    public class LiteratureFormRepository : GenericRepository<LiteratureForm>, ILiteratureFormRepository
    {
        public LiteratureFormRepository(BookStoreContext context) : base(context) { }
    }
}