namespace DAL.EF
{
    using DAL.Entities;
    using System.Data.Entity;

    public class BookStoreContext : DbContext 
    {
        //static OnlineGameStoreContext()
        //{
        //    (new ContextInitializer());
        //}

        public BookStoreContext() : base("BookStoreConnection")
        {
            Database.SetInitializer<BookStoreContext>(new DropCreateDatabaseAlways<BookStoreContext>());

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<LiteratureForm> LiteratureForms { get; set; }
        public DbSet<User> Usres { get; set; }
    }
}