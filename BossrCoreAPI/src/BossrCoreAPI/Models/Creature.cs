using System;
using System.Collections.Generic;
using BossrCoreAPI.Models.Interfaces;

namespace BossrCoreAPI.Models
{
    public class Creature : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int HoursBetweenEachSpawnMin { get; set; }
        public int HoursBetweenEachSpawnMax { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }

        public List<Location> Locations { get; set; }
        public List<Spawn> Spawns { get; set; }
    }
}