﻿using System;
using System.Collections.Generic;
using BossrLib.Interfaces;

namespace BossrLib.Classes
{
    public class World : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Monitored { get; set; }

        public int LastDayDeaths { get; set; }
        public int LastDayKills { get; set; }
        public DateTimeOffset LastScrapeTime { get; set; }

        public List<Spawn> Spawns { get; set; }
    }
}