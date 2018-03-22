namespace DAL.Implementation.Repository
{
    using EF;
    using Entities;
    using Interfaces.Repository;

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BookStoreContext context) : base(context) { }
    }
}