namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuiteСhord",
                c => new
                    {
                        SuiteСhordId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountViews = c.Int(nullable: false),
                        Video = c.String(),
                        SingerId = c.Int(),
                        ApplicationUserId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SuiteСhordId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Singers", t => t.SingerId)
                .Index(t => t.SingerId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Lyrics",
                c => new
                    {
                        LyricsId = c.Int(nullable: false, identity: true),
                        Line = c.String(),
                        SuiteChord_SuiteСhordId = c.Int(),
                    })
                .PrimaryKey(t => t.LyricsId)
                .ForeignKey("dbo.SuiteСhord", t => t.SuiteChord_SuiteСhordId)
                .Index(t => t.SuiteChord_SuiteСhordId);
            
            CreateTable(
                "dbo.Fingerings",
                c => new
                    {
                        FingeringId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.FingeringId);
            
            CreateTable(
                "dbo.Singers",
                c => new
                    {
                        Singerid = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Biography = c.String(),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.Singerid);
            
            CreateTable(
                "dbo.FingeringLyrics",
                c => new
                    {
                        Fingering_FingeringId = c.Int(nullable: false),
                        Lyrics_LyricsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fingering_FingeringId, t.Lyrics_LyricsId })
                .ForeignKey("dbo.Fingerings", t => t.Fingering_FingeringId, cascadeDelete: true)
                .ForeignKey("dbo.Lyrics", t => t.Lyrics_LyricsId, cascadeDelete: true)
                .Index(t => t.Fingering_FingeringId)
                .Index(t => t.Lyrics_LyricsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuiteСhord", "SingerId", "dbo.Singers");
            DropForeignKey("dbo.Lyrics", "SuiteChord_SuiteСhordId", "dbo.SuiteСhord");
            DropForeignKey("dbo.FingeringLyrics", "Lyrics_LyricsId", "dbo.Lyrics");
            DropForeignKey("dbo.FingeringLyrics", "Fingering_FingeringId", "dbo.Fingerings");
            DropForeignKey("dbo.SuiteСhord", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FingeringLyrics", new[] { "Lyrics_LyricsId" });
            DropIndex("dbo.FingeringLyrics", new[] { "Fingering_FingeringId" });
            DropIndex("dbo.Lyrics", new[] { "SuiteChord_SuiteСhordId" });
            DropIndex("dbo.SuiteСhord", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.SuiteСhord", new[] { "SingerId" });
            DropTable("dbo.FingeringLyrics");
            DropTable("dbo.Singers");
            DropTable("dbo.Fingerings");
            DropTable("dbo.Lyrics");
            DropTable("dbo.SuiteСhord");
        }
    }
}
