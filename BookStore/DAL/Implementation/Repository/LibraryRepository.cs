namespace DAL.Implementation.Repository
{
    using EF;
    using Entities;
    using Interfaces.Repository;

    public class LibraryRepository : GenericRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(BookStoreContext context) : base(context) { }
    }
}