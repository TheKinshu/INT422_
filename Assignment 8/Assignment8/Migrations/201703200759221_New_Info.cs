namespace Assignment8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_Info : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coordinator = c.String(),
                        Genre = c.String(),
                        Name = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        UrlAlbum = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BirthName = c.String(),
                        BirthOrStartDate = c.DateTime(nullable: false),
                        Executive = c.String(),
                        Genre = c.String(),
                        Name = c.String(nullable: false),
                        UrlArtist = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Clerk = c.String(),
                        Composers = c.String(),
                        Genre = c.String(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_Id, t.Album_Id })
                .ForeignKey("dbo.Artists", t => t.Artist_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Artist_Id)
                .Index(t => t.Album_Id);
            
            CreateTable(
                "dbo.TrackAlbums",
                c => new
                    {
                        Track_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Track_Id, t.Album_Id })
                .ForeignKey("dbo.Tracks", t => t.Track_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Track_Id)
                .Index(t => t.Album_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrackAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.TrackAlbums", "Track_Id", "dbo.Tracks");
            DropForeignKey("dbo.ArtistAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.ArtistAlbums", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.TrackAlbums", new[] { "Album_Id" });
            DropIndex("dbo.TrackAlbums", new[] { "Track_Id" });
            DropIndex("dbo.ArtistAlbums", new[] { "Album_Id" });
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_Id" });
            DropTable("dbo.TrackAlbums");
            DropTable("dbo.ArtistAlbums");
            DropTable("dbo.Genres");
            DropTable("dbo.Tracks");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
