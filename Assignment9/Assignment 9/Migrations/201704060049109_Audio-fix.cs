namespace Assignment_9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Audiofix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "AudioContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.Tracks", "Audio", c => c.Binary());
            DropColumn("dbo.Tracks", "ClipContentType");
            DropColumn("dbo.Tracks", "Clip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "Clip", c => c.Binary());
            AddColumn("dbo.Tracks", "ClipContentType", c => c.String(maxLength: 200));
            DropColumn("dbo.Tracks", "Audio");
            DropColumn("dbo.Tracks", "AudioContentType");
        }
    }
}
