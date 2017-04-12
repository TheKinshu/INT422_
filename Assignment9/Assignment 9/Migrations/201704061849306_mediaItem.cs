namespace Assignment_9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mediaItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false, maxLength: 200),
                        Content = c.Binary(),
                        ContentType = c.String(nullable: false, maxLength: 200),
                        StringId = c.String(nullable: false, maxLength: 200),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MediaItemArtists",
                c => new
                    {
                        MediaItem_Id = c.Int(nullable: false),
                        Artist_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaItem_Id, t.Artist_Id })
                .ForeignKey("dbo.MediaItems", t => t.MediaItem_Id, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_Id, cascadeDelete: true)
                .Index(t => t.MediaItem_Id)
                .Index(t => t.Artist_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MediaItemArtists", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.MediaItemArtists", "MediaItem_Id", "dbo.MediaItems");
            DropIndex("dbo.MediaItemArtists", new[] { "Artist_Id" });
            DropIndex("dbo.MediaItemArtists", new[] { "MediaItem_Id" });
            DropTable("dbo.MediaItemArtists");
            DropTable("dbo.MediaItems");
        }
    }
}
