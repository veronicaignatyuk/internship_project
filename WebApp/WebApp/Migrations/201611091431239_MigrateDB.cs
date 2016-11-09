namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
           
            AddColumn("dbo.Singers", "LinkToSinger", c => c.String(nullable: false));

        }
        
        public override void Down()
        { 
            DropTable("dbo.Singers");
        }
    }
}
