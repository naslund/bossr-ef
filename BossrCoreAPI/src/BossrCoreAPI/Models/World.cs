﻿using System.Collections.Generic;
using BossrCoreAPI.Models.Interfaces;

namespace BossrCoreAPI.Models
{
    public class World : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Spawn> Spawns { get; set; }
    }
}