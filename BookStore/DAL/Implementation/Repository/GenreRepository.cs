namespace DAL.Implementation.Repository
{
    using EF;
    using Entities;
    using Interfaces.Repository;

    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(BookStoreContext context) : base(context) { }
    }
}