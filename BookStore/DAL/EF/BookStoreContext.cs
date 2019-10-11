namespace DAL.EF
{
    using DTO.Models;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class BookStoreContext : DbContext
    {
        public DbSet<AuthorDto> Authors { get; set; }
        public DbSet<BookDto> Books { get; set; }
        public DbSet<CountryDto> Countries { get; set; }
        public DbSet<GenreDto> Genres { get; set; }
        public DbSet<LibraryDto> Libraries { get; set; }
        public DbSet<LiteratureFormDto> LiteratureForms { get; set; }
        public DbSet<UserDto> Users { get; set; }

        public BookStoreContext() : base("name=BookStoreDb")
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
            modelBuilder.Entity<BookDto>().HasKey(a => a.Id);
            modelBuilder.Entity<BookDto>().Property(b => b.Name).HasMaxLength(50);
            modelBuilder.Entity<BookDto>().Property(b => b.Isbn).HasMaxLength(50);

            modelBuilder.Entity<BookDto>()
                .HasRequired<LibraryDto>(b => b.Library)
                .WithMany(l => l.Books)
                .HasForeignKey<int>(b => b.LibraryId);

            modelBuilder.Entity<BookDto>()
                .HasMany<GenreDto>(b => b.Genres)
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
            modelBuilder.Entity<AuthorDto>().HasKey(a => a.Id);
            modelBuilder.Entity<AuthorDto>().Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Entity<AuthorDto>().Property(a => a.Gender).HasMaxLength(50);

            modelBuilder.Entity<AuthorDto>()
                .HasRequired<CountryDto>(a => a.Country)
                .WithMany(c => c.Authors)
                .HasForeignKey<int>(a => a.CountryId);

            modelBuilder.Entity<AuthorDto>()
                .HasMany<BookDto>(a => a.Books)
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
            modelBuilder.Entity<UserDto>().HasKey(a => a.Id);
            modelBuilder.Entity<UserDto>().Property(u => u.Name).HasMaxLength(50);
            modelBuilder.Entity<UserDto>().Property(u => u.Nickname).HasMaxLength(50);
            modelBuilder.Entity<UserDto>().Property(u => u.Password).HasMaxLength(50);

            modelBuilder.Entity<UserDto>()
                .HasRequired<CountryDto>(u => u.Country)
                .WithMany(c => c.Users)
                .HasForeignKey<int>(u => u.CountryId);

            modelBuilder.Entity<UserDto>()
                .HasRequired<LibraryDto>(u => u.Library)
                .WithMany(l => l.Users)
                .HasForeignKey<int>(u => u.LibraryId);
        }

        private void BuildLiteratureForm(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LiteratureFormDto>().HasKey(a => a.Id);
            modelBuilder.Entity<LiteratureFormDto>().Property(lf => lf.Name).HasMaxLength(50);

            modelBuilder.Entity<LiteratureFormDto>()
                .HasMany<AuthorDto>(l => l.Authors)
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
            modelBuilder.Entity<CountryDto>().HasKey(a => a.Id);
            modelBuilder.Entity<CountryDto>().Property(c => c.Name).HasMaxLength(50);
        }

        private void BuildGenre(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreDto>().HasKey(a => a.Id);
            modelBuilder.Entity<GenreDto>().Property(g => g.Name).HasMaxLength(50);
        }

        private void BuildLibrary(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryDto>().HasKey(a => a.Id);
        }
    }
}