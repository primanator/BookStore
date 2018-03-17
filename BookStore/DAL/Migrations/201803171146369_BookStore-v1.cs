namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookStorev1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Gender = c.String(maxLength: 50),
                        BirthDate = c.DateTime(),
                        DeathDate = c.DateTime(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Isbn = c.String(maxLength: 50),
                        Pages = c.Int(nullable: false),
                        LimitedEdition = c.Boolean(nullable: false),
                        WrittenIn = c.DateTime(),
                        LibraryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Library", t => t.LibraryId, cascadeDelete: true)
                .Index(t => t.LibraryId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Library",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 50),
                        Nickname = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Admin = c.Boolean(nullable: false),
                        Age = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        LibraryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Library", t => t.LibraryId, cascadeDelete: true)
                .Index(t => t.CountryId)
                .Index(t => t.LibraryId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LiteratureForm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookGenre",
                c => new
                    {
                        BookRefId = c.Int(nullable: false),
                        GenreRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookRefId, t.GenreRefId })
                .ForeignKey("dbo.Book", t => t.BookRefId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreRefId, cascadeDelete: true)
                .Index(t => t.BookRefId)
                .Index(t => t.GenreRefId);
            
            CreateTable(
                "dbo.AuthorBook",
                c => new
                    {
                        AuthorRefId = c.Int(nullable: false),
                        BookRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AuthorRefId, t.BookRefId })
                .ForeignKey("dbo.Author", t => t.AuthorRefId, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.BookRefId, cascadeDelete: true)
                .Index(t => t.AuthorRefId)
                .Index(t => t.BookRefId);
            
            CreateTable(
                "dbo.LiteratureFormAuthor",
                c => new
                    {
                        LiteratureFormRefId = c.Int(nullable: false),
                        AuthorRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LiteratureFormRefId, t.AuthorRefId })
                .ForeignKey("dbo.LiteratureForm", t => t.LiteratureFormRefId, cascadeDelete: true)
                .ForeignKey("dbo.Author", t => t.AuthorRefId, cascadeDelete: true)
                .Index(t => t.LiteratureFormRefId)
                .Index(t => t.AuthorRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LiteratureFormAuthor", "AuthorRefId", "dbo.Author");
            DropForeignKey("dbo.LiteratureFormAuthor", "LiteratureFormRefId", "dbo.LiteratureForm");
            DropForeignKey("dbo.Author", "CountryId", "dbo.Country");
            DropForeignKey("dbo.AuthorBook", "BookRefId", "dbo.Book");
            DropForeignKey("dbo.AuthorBook", "AuthorRefId", "dbo.Author");
            DropForeignKey("dbo.Book", "LibraryId", "dbo.Library");
            DropForeignKey("dbo.User", "LibraryId", "dbo.Library");
            DropForeignKey("dbo.User", "CountryId", "dbo.Country");
            DropForeignKey("dbo.BookGenre", "GenreRefId", "dbo.Genre");
            DropForeignKey("dbo.BookGenre", "BookRefId", "dbo.Book");
            DropIndex("dbo.LiteratureFormAuthor", new[] { "AuthorRefId" });
            DropIndex("dbo.LiteratureFormAuthor", new[] { "LiteratureFormRefId" });
            DropIndex("dbo.AuthorBook", new[] { "BookRefId" });
            DropIndex("dbo.AuthorBook", new[] { "AuthorRefId" });
            DropIndex("dbo.BookGenre", new[] { "GenreRefId" });
            DropIndex("dbo.BookGenre", new[] { "BookRefId" });
            DropIndex("dbo.User", new[] { "LibraryId" });
            DropIndex("dbo.User", new[] { "CountryId" });
            DropIndex("dbo.Book", new[] { "LibraryId" });
            DropIndex("dbo.Author", new[] { "CountryId" });
            DropTable("dbo.LiteratureFormAuthor");
            DropTable("dbo.AuthorBook");
            DropTable("dbo.BookGenre");
            DropTable("dbo.LiteratureForm");
            DropTable("dbo.Country");
            DropTable("dbo.User");
            DropTable("dbo.Library");
            DropTable("dbo.Genre");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
