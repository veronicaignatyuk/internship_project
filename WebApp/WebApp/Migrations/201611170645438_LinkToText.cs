namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkToText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SuiteСhord", "LinkToText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SuiteСhord", "LinkToText");
        }
    }
}
