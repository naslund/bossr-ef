﻿using System.Collections.Generic;
using BossrLib.Interfaces;

namespace BossrLib.Classes
{
    public class Location : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public byte PosZ { get; set; }

        public int CreatureId { get; set; }
        public Creature Creature { get; set; }

        public List<Spawn> Spawns { get; set; }
    }
}