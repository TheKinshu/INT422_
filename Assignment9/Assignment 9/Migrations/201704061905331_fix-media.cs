namespace Assignment_9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixmedia : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MediaItemArtists", "MediaItem_Id", "dbo.MediaItems");
            DropForeignKey("dbo.MediaItemArtists", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.MediaItemArtists", new[] { "MediaItem_Id" });
            DropIndex("dbo.MediaItemArtists", new[] { "Artist_Id" });
            AddColumn("dbo.MediaItems", "Artists_Id", c => c.Int());
            CreateIndex("dbo.MediaItems", "Artists_Id");
            AddForeignKey("dbo.MediaItems", "Artists_Id", "dbo.Artists", "Id");
            DropTable("dbo.MediaItemArtists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MediaItemArtists",
                c => new
                    {
                        MediaItem_Id = c.Int(nullable: false),
                        Artist_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaItem_Id, t.Artist_Id });
            
            DropForeignKey("dbo.MediaItems", "Artists_Id", "dbo.Artists");
            DropIndex("dbo.MediaItems", new[] { "Artists_Id" });
            DropColumn("dbo.MediaItems", "Artists_Id");
            CreateIndex("dbo.MediaItemArtists", "Artist_Id");
            CreateIndex("dbo.MediaItemArtists", "MediaItem_Id");
            AddForeignKey("dbo.MediaItemArtists", "Artist_Id", "dbo.Artists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MediaItemArtists", "MediaItem_Id", "dbo.MediaItems", "Id", cascadeDelete: true);
        }
    }
}
