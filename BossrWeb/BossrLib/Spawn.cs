﻿using System;

namespace BossrLib
{
    public class Spawn
    {
        public int Id { get; set; }

        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime DateTimeUtc { get; set; }

        public bool IsExact { get; set; }

        public World World { get; set; }
        public int WorldId { get; set; }

        public Creature Creature { get; set; }
        public int CreatureId { get; set; }
    }
}