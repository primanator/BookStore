namespace DAL.Migrations
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.EF.BookStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.EF.BookStoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.Countries.FirstOrDefault() == null)
                return;

            IEnumerable<Country> countries = new[]
            {
                new Country() { Name = "China" },
                new Country() { Name = "India" },
                new Country() { Name = "United States of America" },
                new Country() { Name = "Indonesia" },
                new Country() { Name = "Brazil" },
                new Country() { Name = "Pakistan" },
                new Country() { Name = "Nigeria" },
                new Country() { Name = "Bangladesh" },
                new Country() { Name = "Russia" },
                new Country() { Name = "Mexico" },
                new Country() { Name = "Japan" },
                new Country() { Name = "Ethiopia" },
                new Country() { Name = "Philippines" },
                new Country() { Name = "Egypt" },
                new Country() { Name = "Viet Nam" },
                new Country() { Name = "DR Congo" },
                new Country() { Name = "Germany" },
                new Country() { Name = "Iran" },
                new Country() { Name = "Turkey" },
                new Country() { Name = "Thailand" },
                new Country() { Name = "United Kingdom" },
                new Country() { Name = "France" },
                new Country() { Name = "Italy" },
                new Country() { Name = "Tanzania" },
                new Country() { Name = "South Africa" },
                new Country() { Name = "Myanmar" },
                new Country() { Name = "South Korea" },
                new Country() { Name = "Kenya" },
                new Country() { Name = "Colombia" },
                new Country() { Name = "Spain" },
                new Country() { Name = "Argentina" },
                new Country() { Name = "Uganda" },
                new Country() { Name = "Ukraine" },
                new Country() { Name = "Algeria" },
                new Country() { Name = "Sudan" },
                new Country() { Name = "Iraq" },
                new Country() { Name = "Poland" },
                new Country() { Name = "Canada" },
                new Country() { Name = "Afghanistan" },
                new Country() { Name = "Morocco" },
                new Country() { Name = "Saudi Arabia" },
                new Country() { Name = "Peru" },
                new Country() { Name = "Venezuela" },
                new Country() { Name = "Uzbekistan" },
                new Country() { Name = "Malaysia" },
                new Country() { Name = "Angola" },
                new Country() { Name = "Mozambique" },
                new Country() { Name = "Nepal" },
                new Country() { Name = "Ghana" },
                new Country() { Name = "Yemen" },
                new Country() { Name = "Madagascar" },
                new Country() { Name = "North Korea" },
                new Country() { Name = "Côte d'Ivoire" },
                new Country() { Name = "Australia" },
                new Country() { Name = "Cameroon" },
                new Country() { Name = "Niger" },
                new Country() { Name = "Sri Lanka" },
                new Country() { Name = "Burkina Faso" },
                new Country() { Name = "Romania" },
                new Country() { Name = "Malawi" },
                new Country() { Name = "Mali" },
                new Country() { Name = "Kazakhstan" },
                new Country() { Name = "Syria" },
                new Country() { Name = "Chile" },
                new Country() { Name = "Zambia" },
                new Country() { Name = "Guatemala" },
                new Country() { Name = "Netherlands" },
                new Country() { Name = "Zimbabwe" },
                new Country() { Name = "Ecuador" },
                new Country() { Name = "Senegal" },
                new Country() { Name = "Cambodia" },
                new Country() { Name = "Chad" },
                new Country() { Name = "Somalia" },
                new Country() { Name = "Guinea" },
                new Country() { Name = "South Sudan" },
                new Country() { Name = "Rwanda" },
                new Country() { Name = "Tunisia" },
                new Country() { Name = "Belgium" },
                new Country() { Name = "Cuba" },
                new Country() { Name = "Benin" },
                new Country() { Name = "Burundi" },
                new Country() { Name = "Bolivia" },
                new Country() { Name = "Greece" },
                new Country() { Name = "Haiti" },
                new Country() { Name = "Dominican Republic" },
                new Country() { Name = "Czech Republic" },
                new Country() { Name = "Portugal" },
                new Country() { Name = "Sweden" },
                new Country() { Name = "Azerbaijan" },
                new Country() { Name = "Jordan" },
                new Country() { Name = "Hungary" },
                new Country() { Name = "United Arab Emirates" },
                new Country() { Name = "Belarus" },
                new Country() { Name = "Honduras" },
                new Country() { Name = "Tajikistan" },
                new Country() { Name = "Serbia" },
                new Country() { Name = "Austria" },
                new Country() { Name = "Switzerland" },
                new Country() { Name = "Israel" },
                new Country() { Name = "Papua New Guinea" },
                new Country() { Name = "Togo" },
                new Country() { Name = "Sierra Leone" },
                new Country() { Name = "Bulgaria" },
                new Country() { Name = "Laos" },
                new Country() { Name = "Paraguay" },
                new Country() { Name = "Libya" },
                new Country() { Name = "El Salvador" },
                new Country() { Name = "Nicaragua" },
                new Country() { Name = "Kyrgyzstan" },
                new Country() { Name = "Lebanon" },
                new Country() { Name = "Turkmenistan" },
                new Country() { Name = "Singapore" },
                new Country() { Name = "Denmark" },
                new Country() { Name = "Finland" },
                new Country() { Name = "Slovakia" },
                new Country() { Name = "Congo" },
                new Country() { Name = "Norway" },
                new Country() { Name = "Eritrea" },
                new Country() { Name = "State of Palestine" },
                new Country() { Name = "Costa Rica" },
                new Country() { Name = "Liberia" },
                new Country() { Name = "Oman" },
                new Country() { Name = "Ireland" },
                new Country() { Name = "New Zealand" },
                new Country() { Name = "Central African Republic" },
                new Country() { Name = "Mauritania" },
                new Country() { Name = "Kuwait" },
                new Country() { Name = "Croatia" },
                new Country() { Name = "Panama" },
                new Country() { Name = "Moldova" },
                new Country() { Name = "Georgia" },
                new Country() { Name = "Bosnia & Herzegovina" },
                new Country() { Name = "Uruguay" },
                new Country() { Name = "Mongolia" },
                new Country() { Name = "Albania" },
                new Country() { Name = "Armenia" },
                new Country() { Name = "Jamaica" },
                new Country() { Name = "Lithuania" },
                new Country() { Name = "Qatar" },
                new Country() { Name = "Namibia" },
                new Country() { Name = "Botswana" },
                new Country() { Name = "Lesotho" },
                new Country() { Name = "Gambia" },
                new Country() { Name = "Macedonia" },
                new Country() { Name = "Slovenia" },
                new Country() { Name = "Gabon" },
                new Country() { Name = "Latvia" },
                new Country() { Name = "Guinea-Bissau" },
                new Country() { Name = "Bahrain" },
                new Country() { Name = "Swaziland" },
                new Country() { Name = "Trinidad and Tobago" },
                new Country() { Name = "Timor-Leste" },
                new Country() { Name = "Equatorial Guinea" },
                new Country() { Name = "Estonia" },
                new Country() { Name = "Mauritius" },
                new Country() { Name = "Cyprus" },
                new Country() { Name = "Djibouti" },
                new Country() { Name = "Fiji" },
                new Country() { Name = "Comoros" },
                new Country() { Name = "Bhutan" },
                new Country() { Name = "Guyana" },
                new Country() { Name = "Montenegro" },
                new Country() { Name = "Solomon Islands" },
                new Country() { Name = "Luxembourg" },
                new Country() { Name = "Suriname" },
                new Country() { Name = "Cabo Verde" },
                new Country() { Name = "Maldives" },
                new Country() { Name = "Brunei" },
                new Country() { Name = "Malta" },
                new Country() { Name = "Bahamas" },
                new Country() { Name = "Belize" },
                new Country() { Name = "Iceland" },
                new Country() { Name = "Barbados" },
                new Country() { Name = "Vanuatu" },
                new Country() { Name = "Sao Tome & Principe" },
                new Country() { Name = "Samoa" },
                new Country() { Name = "Saint Lucia" },
                new Country() { Name = "Kiribati" },
                new Country() { Name = "St. Vincent & Grenadines" },
                new Country() { Name = "Tonga" },
                new Country() { Name = "Grenada" },
                new Country() { Name = "Micronesia" },
                new Country() { Name = "Antigua and Barbuda" },
                new Country() { Name = "Seychelles" },
                new Country() { Name = "Andorra" },
                new Country() { Name = "Dominica" },
                new Country() { Name = "Saint Kitts & Nevis" },
                new Country() { Name = "Marshall Islands" },
                new Country() { Name = "Monaco" },
                new Country() { Name = "Liechtenstein" },
                new Country() { Name = "San Marino" },
                new Country() { Name = "Palau" },
                new Country() { Name = "Nauru" },
                new Country() { Name = "Tuvalu" },
                new Country() { Name = "Holy See" }
            };

            IEnumerable<Author> authors = new[]
            {
                new Author()
                {
                    Name = "Ernest Miller Hemingway",
                    Gender = "Male",
                    BirthDate = new DateTime(1899, 7, 21),
                    DeathDate = new DateTime(1961, 7, 2),
                    Country = countries.FirstOrDefault(c => c.Name == "United States of America"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "United States of America").Id
                },
                new Author()
                {
                    Name = "John Griffith London",
                    Gender = "Male",
                    BirthDate = new DateTime(1876, 1, 12),
                    DeathDate = new DateTime(1916, 11, 22),
                    Country = countries.FirstOrDefault(c => c.Name == "United States of America"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "United States of America").Id
                },
                new Author()
                {
                    Name = "Dame Agatha Mary Clarissa Christie",
                    Gender = "Female",
                    BirthDate = new DateTime(1890, 9, 15),
                    DeathDate = new DateTime(1976, 1, 12),
                    Country = countries.FirstOrDefault(c => c.Name == "United Kingdom"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "United Kingdom").Id
                }
            };

            IEnumerable<Genre> genres = new[]
            {
                new Genre() { Name = "Asemic writing" },
                new Genre() { Name = "Comedy" },
                new Genre() { Name = "Drama" },
                new Genre() { Name = "Horror fiction" },
                new Genre() { Name = "Literary realism" },
                new Genre() { Name = "Romance" },
                new Genre() { Name = "Satire" },
                new Genre() { Name = "Tragedy" },
                new Genre() { Name = "Tragicomedy" },
                new Genre() { Name = "Fantasy" },
                new Genre() { Name = "Mythology" },
                new Genre() { Name = "Adventure" }
            };

            IEnumerable<LiteratureForm> literatureForms = new[]
            {
                new LiteratureForm()
                {
                    Name = "Prose",
                    Authors = new HashSet<Author>(authors)
                },
                new LiteratureForm() { Name = "Poetry" }
            };

            IEnumerable<Library> libraries = new[] { new Library() };

            IEnumerable<Book> books = new[]
            {
                new Book()
                {
                    Title = "Son of the Wolf",
                    Isbn = "0891906541",
                    Pages = 99,
                    LimitedEdition = false,
                    WrittenIn = new DateTime(1911, 1, 1),
                    Library = libraries.FirstOrDefault(),
                    LibraryId = libraries.FirstOrDefault().Id,
                    Authors = new HashSet<Author>(new []
                    {
                        authors.FirstOrDefault(a => a.Name == "John Griffith London")
                    }),
                    Genres = new HashSet<Genre>(new []
                    {
                        genres.FirstOrDefault(g => g.Name == "Adventure")
                    })
                },
                new Book()
                {
                    Title = "The Old Man and the Sea",
                    Isbn = "0684801221",
                    Pages = 127,
                    LimitedEdition = false,
                    WrittenIn = new DateTime(1951, 1, 1),
                    Library = libraries.FirstOrDefault(),
                    LibraryId = libraries.FirstOrDefault().Id,
                    Authors = new HashSet<Author>(new []
                    {
                        authors.FirstOrDefault(a => a.Name == "Ernest Miller Hemingway")
                    }),
                    Genres = new HashSet<Genre>(new []
                    {
                        genres.FirstOrDefault(g => g.Name == "Adventure")
                    })
                },
                new Book()
                {
                    Title = "Murder on the Orient Express",
                    Isbn = "0062073508",
                    Pages = 255,
                    LimitedEdition = false,
                    WrittenIn = new DateTime(1936, 1, 1),
                    Library = libraries.FirstOrDefault(),
                    LibraryId = libraries.FirstOrDefault().Id,
                    Authors = new HashSet<Author>(new []
                    {
                        authors.FirstOrDefault(a => a.Name == "Dame Agatha Mary Clarissa Christie")
                    }),
                    Genres = new HashSet<Genre>(new []
                    {
                        genres.FirstOrDefault(g => g.Name == "Drama")
                    })
                }
            };

            IEnumerable<User> users = new[]
            {
                new User()
                {
                    FullName = "Oleksii Prymolonnyi",
                    Nickname = "primanator",
                    Password = "1234",
                    Admin = true,
                    Age = 23,
                    Country = countries.FirstOrDefault(c => c.Name == "Ukraine"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "Ukraine").Id,
                    Library = libraries.FirstOrDefault(),
                    LibraryId =  libraries.FirstOrDefault().Id,
                },
                new User()
                {
                    FullName = "John Smith",
                    Nickname = "johny",
                    Password = "4321",
                    Admin = false,
                    Age = 17,
                    Country = countries.FirstOrDefault(c => c.Name == "Canada"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "Canada").Id,
                    Library = libraries.FirstOrDefault(),
                    LibraryId =  libraries.FirstOrDefault().Id,
                }
            };

            Author[] authorsArr = authors.ToArray();
            for (int i = 0; i < authorsArr.Length; i++)
            {
                authorsArr[i].LiteratureForms = new HashSet<LiteratureForm>(new[]
                {
                    literatureForms.FirstOrDefault(lf => lf.Name == "Prose")
                });
            }
            authors = authorsArr;

            foreach (Book book in books)
                libraries.FirstOrDefault().Books.Add(book);

            foreach (User user in users)
                libraries.FirstOrDefault().Users.Add(user);


            genres.FirstOrDefault(g => g.Name == "Adventure")
                 .Books.Add(books.FirstOrDefault(b => b.Title == "Son of the Wolf"));

            genres.FirstOrDefault(g => g.Name == "Adventure")
                .Books.Add(books.FirstOrDefault(b => b.Title == "The Old Man and the Sea"));

            genres.FirstOrDefault(g => g.Name == "Drama")
                .Books.Add(books.FirstOrDefault(b => b.Title == "Murder on the Orient Express"));


            countries.FirstOrDefault(c => c.Name == "United States of America")
                .Authors.Add(authors.FirstOrDefault(a => a.Name == "Ernest Miller Hemingway"));

            countries.FirstOrDefault(c => c.Name == "United States of America")
                .Authors.Add(authors.FirstOrDefault(a => a.Name == "John Griffith London"));

            countries.FirstOrDefault(c => c.Name == "United Kingdom")
                .Authors.Add(authors.FirstOrDefault(a => a.Name == "Dame Agatha Mary Clarissa Christie"));

            countries.FirstOrDefault(c => c.Name == "Ukraine")
                .Users.Add(users.FirstOrDefault(u => u.FullName == "Oleksii Prymolonnyi"));

            countries.FirstOrDefault(c => c.Name == "Canada")
                .Users.Add(users.FirstOrDefault(u => u.FullName == "John Smith"));


            authors.FirstOrDefault(a => a.Name == "Ernest Miller Hemingway")
                .Books.Add(books.FirstOrDefault(b => b.Title == "The Old Man and the Sea"));

            authors.FirstOrDefault(a => a.Name == "John Griffith London")
                .Books.Add(books.FirstOrDefault(b => b.Title == "Son of the Wolf"));

            authors.FirstOrDefault(a => a.Name == "Dame Agatha Mary Clarissa Christie")
                .Books.Add(books.FirstOrDefault(b => b.Title == "Murder on the Orient Express"));

            context.Books.AddRange(books);
            context.LiteratureForms.AddRange(literatureForms);
            context.Genres.AddRange(genres);
            context.Libraries.AddRange(libraries);
            context.Users.AddRange(users);
            context.Countries.AddRange(countries);
            context.Authors.AddRange(authors);

            context.SaveChanges();
        }
    }
}