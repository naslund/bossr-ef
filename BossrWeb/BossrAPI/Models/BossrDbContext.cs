using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BossrLib;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BossrAPI.Models
{
    public class BossrDbContext : IdentityDbContext<User, Role, int, BossrUserLogin, BossrUserRole, BossrUserClaim>
    {
        public BossrDbContext() : base("name=BossrDbContext")
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized +=
            (sender, e) => DateTimeKindAttribute.Apply(e.Entity);
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