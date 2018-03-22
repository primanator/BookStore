namespace DAL.Implementation.Repository
{
    using EF;
    using Entities;
    using Interfaces.Repository;

    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(BookStoreContext context) : base(context) { }
    }
}