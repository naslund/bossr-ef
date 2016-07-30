namespace BossrAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeKind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Spawns", "DateTimeUtc", c => c.DateTime(nullable: false));
            DropColumn("dbo.Spawns", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Spawns", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.Spawns", "DateTimeUtc");
        }
    }
}
