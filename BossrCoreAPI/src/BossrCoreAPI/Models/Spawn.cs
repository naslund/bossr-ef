using System;
using BossrCoreAPI.Models.Interfaces;

namespace BossrCoreAPI.Models
{
    public class Spawn : IEntity
    {
        public int Id { get; set; }
        
        public DateTimeOffset DateTimeUtc { get; set; }

        public bool IsExact { get; set; }

        public World World { get; set; }
        public int WorldId { get; set; }

        public Creature Creature { get; set; }
        public int CreatureId { get; set; }
    }
}