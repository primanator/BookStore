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

            if (context.Countries.FirstOrDefault() != null)
                return;

            IEnumerable<CountryDto> countries = new[]
            {
                new CountryDto() { Name = "China" },
                new CountryDto() { Name = "India" },
                new CountryDto() { Name = "United States of America" },
                new CountryDto() { Name = "Indonesia" },
                new CountryDto() { Name = "Brazil" },
                new CountryDto() { Name = "Pakistan" },
                new CountryDto() { Name = "Nigeria" },
                new CountryDto() { Name = "Bangladesh" },
                new CountryDto() { Name = "Russia" },
                new CountryDto() { Name = "Mexico" },
                new CountryDto() { Name = "Japan" },
                new CountryDto() { Name = "Ethiopia" },
                new CountryDto() { Name = "Philippines" },
                new CountryDto() { Name = "Egypt" },
                new CountryDto() { Name = "Viet Nam" },
                new CountryDto() { Name = "DR Congo" },
                new CountryDto() { Name = "Germany" },
                new CountryDto() { Name = "Iran" },
                new CountryDto() { Name = "Turkey" },
                new CountryDto() { Name = "Thailand" },
                new CountryDto() { Name = "United Kingdom" },
                new CountryDto() { Name = "France" },
                new CountryDto() { Name = "Italy" },
                new CountryDto() { Name = "Tanzania" },
                new CountryDto() { Name = "South Africa" },
                new CountryDto() { Name = "Myanmar" },
                new CountryDto() { Name = "South Korea" },
                new CountryDto() { Name = "Kenya" },
                new CountryDto() { Name = "Colombia" },
                new CountryDto() { Name = "Spain" },
                new CountryDto() { Name = "Argentina" },
                new CountryDto() { Name = "Uganda" },
                new CountryDto() { Name = "Ukraine" },
                new CountryDto() { Name = "Algeria" },
                new CountryDto() { Name = "Sudan" },
                new CountryDto() { Name = "Iraq" },
                new CountryDto() { Name = "Poland" },
                new CountryDto() { Name = "Canada" },
                new CountryDto() { Name = "Afghanistan" },
                new CountryDto() { Name = "Morocco" },
                new CountryDto() { Name = "Saudi Arabia" },
                new CountryDto() { Name = "Peru" },
                new CountryDto() { Name = "Venezuela" },
                new CountryDto() { Name = "Uzbekistan" },
                new CountryDto() { Name = "Malaysia" },
                new CountryDto() { Name = "Angola" },
                new CountryDto() { Name = "Mozambique" },
                new CountryDto() { Name = "Nepal" },
                new CountryDto() { Name = "Ghana" },
                new CountryDto() { Name = "Yemen" },
                new CountryDto() { Name = "Madagascar" },
                new CountryDto() { Name = "North Korea" },
                new CountryDto() { Name = "Côte d'Ivoire" },
                new CountryDto() { Name = "Australia" },
                new CountryDto() { Name = "Cameroon" },
                new CountryDto() { Name = "Niger" },
                new CountryDto() { Name = "Sri Lanka" },
                new CountryDto() { Name = "Burkina Faso" },
                new CountryDto() { Name = "Romania" },
                new CountryDto() { Name = "Malawi" },
                new CountryDto() { Name = "Mali" },
                new CountryDto() { Name = "Kazakhstan" },
                new CountryDto() { Name = "Syria" },
                new CountryDto() { Name = "Chile" },
                new CountryDto() { Name = "Zambia" },
                new CountryDto() { Name = "Guatemala" },
                new CountryDto() { Name = "Netherlands" },
                new CountryDto() { Name = "Zimbabwe" },
                new CountryDto() { Name = "Ecuador" },
                new CountryDto() { Name = "Senegal" },
                new CountryDto() { Name = "Cambodia" },
                new CountryDto() { Name = "Chad" },
                new CountryDto() { Name = "Somalia" },
                new CountryDto() { Name = "Guinea" },
                new CountryDto() { Name = "South Sudan" },
                new CountryDto() { Name = "Rwanda" },
                new CountryDto() { Name = "Tunisia" },
                new CountryDto() { Name = "Belgium" },
                new CountryDto() { Name = "Cuba" },
                new CountryDto() { Name = "Benin" },
                new CountryDto() { Name = "Burundi" },
                new CountryDto() { Name = "Bolivia" },
                new CountryDto() { Name = "Greece" },
                new CountryDto() { Name = "Haiti" },
                new CountryDto() { Name = "Dominican Republic" },
                new CountryDto() { Name = "Czech Republic" },
                new CountryDto() { Name = "Portugal" },
                new CountryDto() { Name = "Sweden" },
                new CountryDto() { Name = "Azerbaijan" },
                new CountryDto() { Name = "Jordan" },
                new CountryDto() { Name = "Hungary" },
                new CountryDto() { Name = "United Arab Emirates" },
                new CountryDto() { Name = "Belarus" },
                new CountryDto() { Name = "Honduras" },
                new CountryDto() { Name = "Tajikistan" },
                new CountryDto() { Name = "Serbia" },
                new CountryDto() { Name = "Austria" },
                new CountryDto() { Name = "Switzerland" },
                new CountryDto() { Name = "Israel" },
                new CountryDto() { Name = "Papua New Guinea" },
                new CountryDto() { Name = "Togo" },
                new CountryDto() { Name = "Sierra Leone" },
                new CountryDto() { Name = "Bulgaria" },
                new CountryDto() { Name = "Laos" },
                new CountryDto() { Name = "Paraguay" },
                new CountryDto() { Name = "Libya" },
                new CountryDto() { Name = "El Salvador" },
                new CountryDto() { Name = "Nicaragua" },
                new CountryDto() { Name = "Kyrgyzstan" },
                new CountryDto() { Name = "Lebanon" },
                new CountryDto() { Name = "Turkmenistan" },
                new CountryDto() { Name = "Singapore" },
                new CountryDto() { Name = "Denmark" },
                new CountryDto() { Name = "Finland" },
                new CountryDto() { Name = "Slovakia" },
                new CountryDto() { Name = "Congo" },
                new CountryDto() { Name = "Norway" },
                new CountryDto() { Name = "Eritrea" },
                new CountryDto() { Name = "State of Palestine" },
                new CountryDto() { Name = "Costa Rica" },
                new CountryDto() { Name = "Liberia" },
                new CountryDto() { Name = "Oman" },
                new CountryDto() { Name = "Ireland" },
                new CountryDto() { Name = "New Zealand" },
                new CountryDto() { Name = "Central African Republic" },
                new CountryDto() { Name = "Mauritania" },
                new CountryDto() { Name = "Kuwait" },
                new CountryDto() { Name = "Croatia" },
                new CountryDto() { Name = "Panama" },
                new CountryDto() { Name = "Moldova" },
                new CountryDto() { Name = "Georgia" },
                new CountryDto() { Name = "Bosnia & Herzegovina" },
                new CountryDto() { Name = "Uruguay" },
                new CountryDto() { Name = "Mongolia" },
                new CountryDto() { Name = "Albania" },
                new CountryDto() { Name = "Armenia" },
                new CountryDto() { Name = "Jamaica" },
                new CountryDto() { Name = "Lithuania" },
                new CountryDto() { Name = "Qatar" },
                new CountryDto() { Name = "Namibia" },
                new CountryDto() { Name = "Botswana" },
                new CountryDto() { Name = "Lesotho" },
                new CountryDto() { Name = "Gambia" },
                new CountryDto() { Name = "Macedonia" },
                new CountryDto() { Name = "Slovenia" },
                new CountryDto() { Name = "Gabon" },
                new CountryDto() { Name = "Latvia" },
                new CountryDto() { Name = "Guinea-Bissau" },
                new CountryDto() { Name = "Bahrain" },
                new CountryDto() { Name = "Swaziland" },
                new CountryDto() { Name = "Trinidad and Tobago" },
                new CountryDto() { Name = "Timor-Leste" },
                new CountryDto() { Name = "Equatorial Guinea" },
                new CountryDto() { Name = "Estonia" },
                new CountryDto() { Name = "Mauritius" },
                new CountryDto() { Name = "Cyprus" },
                new CountryDto() { Name = "Djibouti" },
                new CountryDto() { Name = "Fiji" },
                new CountryDto() { Name = "Comoros" },
                new CountryDto() { Name = "Bhutan" },
                new CountryDto() { Name = "Guyana" },
                new CountryDto() { Name = "Montenegro" },
                new CountryDto() { Name = "Solomon Islands" },
                new CountryDto() { Name = "Luxembourg" },
                new CountryDto() { Name = "Suriname" },
                new CountryDto() { Name = "Cabo Verde" },
                new CountryDto() { Name = "Maldives" },
                new CountryDto() { Name = "Brunei" },
                new CountryDto() { Name = "Malta" },
                new CountryDto() { Name = "Bahamas" },
                new CountryDto() { Name = "Belize" },
                new CountryDto() { Name = "Iceland" },
                new CountryDto() { Name = "Barbados" },
                new CountryDto() { Name = "Vanuatu" },
                new CountryDto() { Name = "Sao Tome & Principe" },
                new CountryDto() { Name = "Samoa" },
                new CountryDto() { Name = "Saint Lucia" },
                new CountryDto() { Name = "Kiribati" },
                new CountryDto() { Name = "St. Vincent & Grenadines" },
                new CountryDto() { Name = "Tonga" },
                new CountryDto() { Name = "Grenada" },
                new CountryDto() { Name = "Micronesia" },
                new CountryDto() { Name = "Antigua and Barbuda" },
                new CountryDto() { Name = "Seychelles" },
                new CountryDto() { Name = "Andorra" },
                new CountryDto() { Name = "Dominica" },
                new CountryDto() { Name = "Saint Kitts & Nevis" },
                new CountryDto() { Name = "Marshall Islands" },
                new CountryDto() { Name = "Monaco" },
                new CountryDto() { Name = "Liechtenstein" },
                new CountryDto() { Name = "San Marino" },
                new CountryDto() { Name = "Palau" },
                new CountryDto() { Name = "Nauru" },
                new CountryDto() { Name = "Tuvalu" },
                new CountryDto() { Name = "Holy See" }
            };

            IEnumerable<AuthorDto> authors = new[]
            {
                new AuthorDto()
                {
                    Name = "Ernest Miller Hemingway",
                    Gender = "Male",
                    BirthDate = new DateTime(1899, 7, 21),
                    DeathDate = new DateTime(1961, 7, 2),
                    Country = countries.FirstOrDefault(c => c.Name == "United States of America"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "United States of America").Id
                },
                new AuthorDto()
                {
                    Name = "John Griffith London",
                    Gender = "Male",
                    BirthDate = new DateTime(1876, 1, 12),
                    DeathDate = new DateTime(1916, 11, 22),
                    Country = countries.FirstOrDefault(c => c.Name == "United States of America"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "United States of America").Id
                },
                new AuthorDto()
                {
                    Name = "Dame Agatha Mary Clarissa Christie",
                    Gender = "Female",
                    BirthDate = new DateTime(1890, 9, 15),
                    DeathDate = new DateTime(1976, 1, 12),
                    Country = countries.FirstOrDefault(c => c.Name == "United Kingdom"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "United Kingdom").Id
                }
            };

            IEnumerable<GenreDto> genres = new[]
            {
                new GenreDto() { Name = "Asemic writing" },
                new GenreDto() { Name = "Comedy" },
                new GenreDto() { Name = "Drama" },
                new GenreDto() { Name = "Horror fiction" },
                new GenreDto() { Name = "Literary realism" },
                new GenreDto() { Name = "Romance" },
                new GenreDto() { Name = "Satire" },
                new GenreDto() { Name = "Tragedy" },
                new GenreDto() { Name = "Tragicomedy" },
                new GenreDto() { Name = "Fantasy" },
                new GenreDto() { Name = "Mythology" },
                new GenreDto() { Name = "Adventure" }
            };

            IEnumerable<LiteratureFormDto> literatureForms = new[]
            {
                new LiteratureFormDto()
                {
                    Name = "Prose",
                    Authors = new HashSet<AuthorDto>(authors)
                },
                new LiteratureFormDto() { Name = "Poetry" }
            };

            IEnumerable<LibraryDto> libraries = new[] { new LibraryDto() };

            IEnumerable<BookDto> books = new[]
            {
                new BookDto()
                {
                    Name = "Son of the Wolf",
                    Isbn = "0891906541",
                    Pages = 99,
                    LimitedEdition = false,
                    WrittenIn = new DateTime(1911, 1, 1),
                    Library = libraries.FirstOrDefault(),
                    LibraryId = libraries.FirstOrDefault().Id,
                    Authors = new HashSet<AuthorDto>(new []
                    {
                        authors.FirstOrDefault(a => a.Name == "John Griffith London")
                    }),
                    Genres = new HashSet<GenreDto>(new []
                    {
                        genres.FirstOrDefault(g => g.Name == "Adventure")
                    })
                },
                new BookDto()
                {
                    Name = "The Old Man and the Sea",
                    Isbn = "0684801221",
                    Pages = 127,
                    LimitedEdition = false,
                    WrittenIn = new DateTime(1951, 1, 1),
                    Library = libraries.FirstOrDefault(),
                    LibraryId = libraries.FirstOrDefault().Id,
                    Authors = new HashSet<AuthorDto>(new []
                    {
                        authors.FirstOrDefault(a => a.Name == "Ernest Miller Hemingway")
                    }),
                    Genres = new HashSet<GenreDto>(new []
                    {
                        genres.FirstOrDefault(g => g.Name == "Adventure")
                    })
                },
                new BookDto()
                {
                    Name = "Murder on the Orient Express",
                    Isbn = "0062073508",
                    Pages = 255,
                    LimitedEdition = false,
                    WrittenIn = new DateTime(1936, 1, 1),
                    Library = libraries.FirstOrDefault(),
                    LibraryId = libraries.FirstOrDefault().Id,
                    Authors = new HashSet<AuthorDto>(new []
                    {
                        authors.FirstOrDefault(a => a.Name == "Dame Agatha Mary Clarissa Christie")
                    }),
                    Genres = new HashSet<GenreDto>(new []
                    {
                        genres.FirstOrDefault(g => g.Name == "Drama")
                    })
                }
            };

            IEnumerable<UserDto> users = new[]
            {
                new UserDto()
                {
                    Name = "Oleksii Prymolonnyi",
                    Nickname = "primanator",
                    Password = "1234",
                    Admin = true,
                    Age = 23,
                    Country = countries.FirstOrDefault(c => c.Name == "Ukraine"),
                    CountryId = countries.FirstOrDefault(c => c.Name == "Ukraine").Id,
                    Library = libraries.FirstOrDefault(),
                    LibraryId =  libraries.FirstOrDefault().Id,
                },
                new UserDto()
                {
                    Name = "John Smith",
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

            AuthorDto[] authorsArr = authors.ToArray();
            for (int i = 0; i < authorsArr.Length; i++)
            {
                authorsArr[i].LiteratureForms = new HashSet<LiteratureFormDto>(new[]
                {
                    literatureForms.FirstOrDefault(lf => lf.Name == "Prose")
                });
            }
            authors = authorsArr;

            foreach (BookDto book in books)
                libraries.FirstOrDefault().Books.Add(book);

            foreach (UserDto user in users)
                libraries.FirstOrDefault().Users.Add(user);


            genres.FirstOrDefault(g => g.Name == "Adventure")
                 .Books.Add(books.FirstOrDefault(b => b.Name == "Son of the Wolf"));

            genres.FirstOrDefault(g => g.Name == "Adventure")
                .Books.Add(books.FirstOrDefault(b => b.Name == "The Old Man and the Sea"));

            genres.FirstOrDefault(g => g.Name == "Drama")
                .Books.Add(books.FirstOrDefault(b => b.Name == "Murder on the Orient Express"));


            countries.FirstOrDefault(c => c.Name == "United States of America")
                .Authors.Add(authors.FirstOrDefault(a => a.Name == "Ernest Miller Hemingway"));

            countries.FirstOrDefault(c => c.Name == "United States of America")
                .Authors.Add(authors.FirstOrDefault(a => a.Name == "John Griffith London"));

            countries.FirstOrDefault(c => c.Name == "United Kingdom")
                .Authors.Add(authors.FirstOrDefault(a => a.Name == "Dame Agatha Mary Clarissa Christie"));

            countries.FirstOrDefault(c => c.Name == "Ukraine")
                .Users.Add(users.FirstOrDefault(u => u.Name == "Oleksii Prymolonnyi"));

            countries.FirstOrDefault(c => c.Name == "Canada")
                .Users.Add(users.FirstOrDefault(u => u.Name == "John Smith"));


            authors.FirstOrDefault(a => a.Name == "Ernest Miller Hemingway")
                .Books.Add(books.FirstOrDefault(b => b.Name == "The Old Man and the Sea"));

            authors.FirstOrDefault(a => a.Name == "John Griffith London")
                .Books.Add(books.FirstOrDefault(b => b.Name == "Son of the Wolf"));

            authors.FirstOrDefault(a => a.Name == "Dame Agatha Mary Clarissa Christie")
                .Books.Add(books.FirstOrDefault(b => b.Name == "Murder on the Orient Express"));

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