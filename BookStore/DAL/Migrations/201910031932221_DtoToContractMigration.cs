namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DtoToContractMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable("Author", "AuthorDto");
            RenameTable("Book", "BookDto");
            RenameTable("Country", "CountryDto");
            RenameTable("Genre", "GenreDto");
            RenameTable("Library", "LibraryDto");
            RenameTable("LiteratureForm", "LiteratureFormDto");
            RenameTable("User", "UserDto");
        }

        public override void Down()
        {
            RenameTable("AuthorDto", "Author");
            RenameTable("BookDto", "Book");
            RenameTable("CountryDto", "Country");
            RenameTable("GenreDto", "Genre");
            RenameTable("LibraryDto", "Library");
            RenameTable("LiteratureFormDto", "LiteratureForm");
            RenameTable("UserDto", "User");
        }
    }
}
