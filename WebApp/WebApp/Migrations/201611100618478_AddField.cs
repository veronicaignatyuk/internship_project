namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SuiteСhord", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SuiteСhord", "Text");
        }
    }
}
