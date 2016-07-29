using System.Data.Entity;
using BossrLib;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BossrAPI.Models
{
    public class BossrDbContext : IdentityDbContext<ApplicationUser>
    {
        public BossrDbContext() : base("name=BossrDbContext")
        {
        }

        public static BossrDbContext Create()
        {
            return new BossrDbContext();
        }

        public virtual DbSet<World> Worlds { get; set; }
        public virtual DbSet<Creature> Creatures { get; set; }
        public virtual DbSet<Spawn> Spawn { get; set; }
    }
}