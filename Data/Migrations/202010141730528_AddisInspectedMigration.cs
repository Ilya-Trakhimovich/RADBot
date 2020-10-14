namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisInspectedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StreetRoads", "isInspected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StreetRoads", "isInspected");
        }
    }
}
