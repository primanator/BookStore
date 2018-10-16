namespace DAL.EF
{
    using DTO_EF.Entities;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class BookStoreContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<LiteratureForm> LiteratureForms { get; set; }
        public DbSet<User> Users { get; set; }

        public BookStoreContext() : base("BookStoreDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookStoreContext, Migrations.Configuration>());

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            BuildAuthor(modelBuilder);
            BuildBook(modelBuilder);
            BuildCountry(modelBuilder);
            BuildGenre(modelBuilder);
            BuildLibrary(modelBuilder);
            BuildLiteratureForm(modelBuilder);
            BuildUser(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void BuildBook(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(a => a.Id);
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Isbn).HasMaxLength(50);

            modelBuilder.Entity<Book>()
                .HasRequired<Library>(b => b.Library)
                .WithMany(l => l.Books)
                .HasForeignKey<int>(b => b.LibraryId);

            modelBuilder.Entity<Book>()
                .HasMany<Genre>(b => b.Genres)
                .WithMany(g => g.Books)
                .Map(bg =>
                {
                    bg.MapLeftKey("BookRefId");
                    bg.MapRightKey("GenreRefId");
                    bg.ToTable("BookGenre");
                });
        }

        private void BuildAuthor(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.Gender).HasMaxLength(50);

            modelBuilder.Entity<Author>()
                .HasRequired<Country>(a => a.Country)
                .WithMany(c => c.Authors)
                .HasForeignKey<int>(a => a.CountryId);

            modelBuilder.Entity<Author>()
                .HasMany<Book>(a => a.Books)
                .WithMany(b => b.Authors)
                .Map(ab =>
                {
                    ab.MapLeftKey("AuthorRefId");
                    ab.MapRightKey("BookRefId");
                    ab.ToTable("AuthorBook");
                });
        }

        private void BuildUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(a => a.Id);
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Nickname).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Password).HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasRequired<Country>(u => u.Country)
                .WithMany(c => c.Users)
                .HasForeignKey<int>(u => u.CountryId);

            modelBuilder.Entity<User>()
                .HasRequired<Library>(u => u.Library)
                .WithMany(l => l.Users)
                .HasForeignKey<int>(u => u.LibraryId);
        }

        private void BuildLiteratureForm(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LiteratureForm>().HasKey(a => a.Id);
            modelBuilder.Entity<LiteratureForm>().Property(lf => lf.Name).HasMaxLength(50);

            modelBuilder.Entity<LiteratureForm>()
                .HasMany<Author>(l => l.Authors)
                .WithMany(a => a.LiteratureForms)
                .Map(la =>
                {
                    la.MapLeftKey("LiteratureFormRefId");
                    la.MapRightKey("AuthorRefId");
                    la.ToTable("LiteratureFormAuthor");
                });
        }

        private void BuildCountry(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(a => a.Id);
            modelBuilder.Entity<Country>().Property(c => c.Name).HasMaxLength(50);
        }

        private void BuildGenre(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasKey(a => a.Id);
            modelBuilder.Entity<Genre>().Property(g => g.Name).HasMaxLength(50);
        }

        private void BuildLibrary(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>().HasKey(a => a.Id);
        }
    }
}