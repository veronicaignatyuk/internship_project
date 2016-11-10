namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldcountViews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Singers", "CountViews", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Singers", "CountViews");
        }
    }
}
