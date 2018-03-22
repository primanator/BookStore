namespace DAL.Implementation.Repository
{
    using EF;
    using Entities;
    using Interfaces.Repository;

    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookStoreContext context) : base(context) { }
    }
}