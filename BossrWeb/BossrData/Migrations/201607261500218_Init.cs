using System.Data.Entity.Migrations;

namespace BossrData.Migrations
{
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Creatures",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String()
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Spawns",
                c => new
                {
                    Id = c.Int(false, true),
                    Time = c.DateTime(false),
                    IsExact = c.Boolean(false),
                    WorldId = c.Int(false),
                    CreatureId = c.Int(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creatures", t => t.CreatureId, true)
                .ForeignKey("dbo.Worlds", t => t.WorldId, true)
                .Index(t => t.WorldId)
                .Index(t => t.CreatureId);

            CreateTable(
                "dbo.Worlds",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String()
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Spawns", "WorldId", "dbo.Worlds");
            DropForeignKey("dbo.Spawns", "CreatureId", "dbo.Creatures");
            DropIndex("dbo.Spawns", new[] {"CreatureId"});
            DropIndex("dbo.Spawns", new[] {"WorldId"});
            DropTable("dbo.Worlds");
            DropTable("dbo.Spawns");
            DropTable("dbo.Creatures");
        }
    }
}