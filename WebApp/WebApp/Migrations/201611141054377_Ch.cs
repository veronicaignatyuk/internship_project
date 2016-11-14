namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ch : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SuiteСhord", "CountViews", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SuiteСhord", "CountViews", c => c.Int(nullable: false));
        }
    }
}
