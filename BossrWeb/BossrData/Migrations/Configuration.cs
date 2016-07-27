using System.Data.Entity.Migrations;

namespace BossrData.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BossrContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BossrContext context)
        {
        }
    }
}