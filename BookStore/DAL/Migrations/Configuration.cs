namespace DAL.Migrations
{
    using DTO.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EF.BookStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EF.BookStoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.Books.FirstOrDefault() != null)
                return;

            IEnumerable<BookDto> books = new[]
            {
                new BookDto()
                {
                    Name = "Son of the Wolf",
                    Isbn = "0891906541",
                    Pages = 99,
                    LimitedEdition = false
                },
                new BookDto()
                {
                    Name = "The Old Man and the Sea",
                    Isbn = "0684801221",
                    Pages = 127,
                    LimitedEdition = false
                },
                new BookDto()
                {
                    Name = "Murder on the Orient Express",
                    Isbn = "0062073508",
                    Pages = 255,
                    LimitedEdition = false
                }
            };
            context.Books.AddRange(books);

            context.SaveChanges();
        }
    }
}