namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using Interface;

    public class BookStoreService : IBookStoreService
    {
        public void DeleteAuthor(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(string title)
        {
            throw new NotImplementedException();
        }

        public void DeleteCountry(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteGenre(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteLibrary()
        {
            throw new NotImplementedException();
        }

        public void DeleteLiteratureForm(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorDTO> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookDTO> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CountryDTO> GetAllCountries()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenreDTO> GetAllGenres()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LibraryDTO> GetAllLibraries()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LiteratureFormDTO> GetAllLiteratureForms()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public AuthorDTO GetAuthor(string name)
        {
            throw new NotImplementedException();
        }

        public BookDTO GetBook(string title)
        {
            throw new NotImplementedException();
        }

        public CountryDTO GetCountry(string name)
        {
            throw new NotImplementedException();
        }

        public GenreDTO GetGenre(string name)
        {
            throw new NotImplementedException();
        }

        public LibraryDTO GetLibrary()
        {
            throw new NotImplementedException();
        }

        public LiteratureFormDTO GetLiteratureForm(string name)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public void ManipulateAuthor(string name, string gender, DateTime? birthdate, DateTime? deathdate, int countryId, ICollection<LiteratureFormDTO> literatureForms, ICollection<BookDTO> books, bool edit = false)
        {
            throw new NotImplementedException();
        }

        public void ManipulateBook(string title, string isbn, int pages, bool limitedEdition, DateTime? writtenIn, int libraryId, ICollection<AuthorDTO> authors, ICollection<GenreDTO> genres, bool edit = false)
        {
            throw new NotImplementedException();
        }

        public void ManipulateCountry(string name, ICollection<UserDTO> users, ICollection<AuthorDTO> authors, bool edit = false)
        {
            throw new NotImplementedException();
        }

        public void ManipulateeGenre(string name, ICollection<BookDTO> books, bool edit = false)
        {
            throw new NotImplementedException();
        }

        public void ManipulateLiteratureForm(string name, ICollection<AuthorDTO> authors, bool edit = false)
        {
            throw new NotImplementedException();
        }

        public void ManipulateUser(string fullname, string nickname, string password, bool admin, int age, int countryId, int libraryId, bool edit = false)
        {
            throw new NotImplementedException();
        }
    }
}
