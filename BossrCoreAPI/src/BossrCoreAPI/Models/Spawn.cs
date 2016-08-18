using System;
using BossrCoreAPI.Models.Interfaces;

namespace BossrCoreAPI.Models
{
    public class Spawn : IEntity
    {
        public int Id { get; set; }
        
        public DateTimeOffset TimeMinUtc { get; set; }
        public DateTimeOffset TimeMaxUtc { get; set; }

        public World World { get; set; }
        public int WorldId { get; set; }

        public Creature Creature { get; set; }
        public int CreatureId { get; set; }

        public Location Location { get; set; }
        public int? LocationId { get; set; }
    }
}