namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SuiteСhord", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.SuiteСhord", "ApplicationUserId");
            RenameColumn(table: "dbo.SuiteСhord", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.SuiteСhord", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.SuiteСhord", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SuiteСhord", new[] { "ApplicationUserId" });
            AlterColumn("dbo.SuiteСhord", "ApplicationUserId", c => c.Int());
            RenameColumn(table: "dbo.SuiteСhord", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.SuiteСhord", "ApplicationUserId", c => c.Int());
            CreateIndex("dbo.SuiteСhord", "ApplicationUser_Id");
        }
    }
}
