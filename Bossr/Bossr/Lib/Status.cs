using System;
using System.Collections.Generic;

namespace Bossr.Lib
{
    public class PossibleSpawnPoint
    {
        public string Name { get; set; }
        public string FullPos { get; set; }
    }

    public class Status
    {
        public Guid CreatureId { get; set; }
        public string CreatureName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryColorRGB { get; set; }
        public List<PossibleSpawnPoint> PossibleSpawnPoints { get; set; }
        public int MissedSpawns { get; set; }
        public bool CanSpawn { get; set; }
        public int PercentElapsed { get; set; }
        public DateTime ExpectedSpawnDateMin { get; set; }
        public DateTime ExpectedSpawnDateMax { get; set; }

        public string TimeLeft => $"{Math.Ceiling((DateTime.SpecifyKind(ExpectedSpawnDateMax, DateTimeKind.Utc) - DateTime.UtcNow).TotalHours)} hours left";
    }
}