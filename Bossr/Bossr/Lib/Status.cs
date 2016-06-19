using System;
using System.Collections.Generic;
using System.Globalization;
using Humanizer;
using Xamarin.Forms;

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

        public string TimeLeftH => $"{(DateTime.SpecifyKind(ExpectedSpawnDateMax, DateTimeKind.Utc) - DateTime.UtcNow).Humanize(2, CultureInfo.InvariantCulture)} remaining";

        public Color CategoryColor
        {
            get
            {
                string[] colors = CategoryColorRGB.Split(',');
                return Color.FromRgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
            }
        }
    }
}