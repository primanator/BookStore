namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookWrittenInType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "WrittenIn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "WrittenIn", c => c.DateTime());
        }
    }
}
