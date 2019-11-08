namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookDto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Isbn = c.String(maxLength: 50),
                        Pages = c.Int(nullable: false),
                        LimitedEdition = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookDto");
        }
    }
}
