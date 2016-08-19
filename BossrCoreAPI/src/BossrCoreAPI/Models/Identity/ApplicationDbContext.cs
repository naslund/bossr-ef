using Microsoft.EntityFrameworkCore;
using OpenIddict;
using BossrLib.Classes;

namespace BossrCoreAPI.Models.Identity
{
    public class ApplicationDbContext : OpenIddictDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Creature> Creatures { get; set; }
        public DbSet<World> Worlds { get; set; }
        public DbSet<Spawn> Spawns { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
