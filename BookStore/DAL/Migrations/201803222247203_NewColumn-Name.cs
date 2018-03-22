namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumnName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.Library", "Name", c => c.String());
            AddColumn("dbo.User", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.Book", "Title");
            DropColumn("dbo.User", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "FullName", c => c.String(maxLength: 50));
            AddColumn("dbo.Book", "Title", c => c.String(maxLength: 50));
            DropColumn("dbo.User", "Name");
            DropColumn("dbo.Library", "Name");
            DropColumn("dbo.Book", "Name");
        }
    }
}
