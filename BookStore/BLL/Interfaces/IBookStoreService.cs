namespace BLL.Interface
{
    using System;
    using System.Collections.Generic;
    using DTO;

    public interface IBookStoreService
    {
        void ManipulateAuthor(string name, string gender, DateTime? birthdate, DateTime? deathdate, int countryId, ICollection<LiteratureFormDTO> literatureForms, ICollection<BookDTO> books, bool edit = false);
        void ManipulateBook(string title, string isbn, int pages, bool limitedEdition, DateTime? writtenIn, int libraryId, ICollection<AuthorDTO> authors, ICollection<GenreDTO> genres, bool edit = false);
        void ManipulateCountry(string name, ICollection<UserDTO> users, ICollection<AuthorDTO> authors, bool edit = false);
        void ManipulateeGenre(string name, ICollection<BookDTO> books, bool edit = false);
        void ManipulateLiteratureForm(string name, ICollection<AuthorDTO> authors, bool edit = false);
        void ManipulateUser(string fullname, string nickname, string password, bool admin, int age, int countryId, int libraryId, bool edit = false);

        IEnumerable<AuthorDTO> GetAllAuthors();
        IEnumerable<BookDTO> GetAllBooks();
        IEnumerable<CountryDTO> GetAllCountries();
        IEnumerable<GenreDTO> GetAllGenres();
        IEnumerable<LibraryDTO> GetAllLibraries();
        IEnumerable<LiteratureFormDTO> GetAllLiteratureForms();
        IEnumerable<UserDTO> GetAllUsers();

        AuthorDTO GetAuthor(string name);
        BookDTO GetBook(string title);
        CountryDTO GetCountry(string name);
        GenreDTO GetGenre(string name);
        LibraryDTO GetLibrary();
        LiteratureFormDTO GetLiteratureForm(string name);
        UserDTO GetUser(string name);
        
        void DeleteAuthor(string name);
        void DeleteBook(string title);
        void DeleteCountry(string name);
        void DeleteGenre(string name);
        void DeleteLibrary();
        void DeleteLiteratureForm(string name);
        void DeleteUser(string name);
    }
}