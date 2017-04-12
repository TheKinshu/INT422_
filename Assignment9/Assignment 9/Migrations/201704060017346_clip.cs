namespace Assignment_9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "ClipContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.Tracks", "Clip", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "Clip");
            DropColumn("dbo.Tracks", "ClipContentType");
        }
    }
}
