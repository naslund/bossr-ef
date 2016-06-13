using System;

namespace Bossr.Lib
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class PvpType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class World
    {
        public Location Location { get; set; }
        public PvpType PvpType { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Monitored { get; set; }
        public int LastDayDeaths { get; set; }
        public int LastDayKills { get; set; }
        public DateTime LastScrape { get; set; }
    }
}