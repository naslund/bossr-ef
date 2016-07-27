using System.Data.Entity.Migrations;

namespace BossrData.Migrations
{
    public partial class Account : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                {
                    Id = c.Int(false, true),
                    Username = c.String(),
                    Password = c.String()
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}