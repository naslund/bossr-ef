using System.Data.Entity;
using BossrLib;

namespace BossrData
{
    public class BossrContext : DbContext
    {
        public BossrContext()
            : base("name=BossrContext")
        {
        }

        public virtual DbSet<World> Worlds { get; set; }
        public virtual DbSet<Creature> Creatures { get; set; }
        public virtual DbSet<Spawn> Spawn { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }
    }
}