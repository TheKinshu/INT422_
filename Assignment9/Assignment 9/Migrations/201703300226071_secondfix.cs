namespace Assignment_9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Depiction", c => c.String());
            DropColumn("dbo.Albums", "Portrayal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Portrayal", c => c.String());
            DropColumn("dbo.Albums", "Depiction");
        }
    }
}
