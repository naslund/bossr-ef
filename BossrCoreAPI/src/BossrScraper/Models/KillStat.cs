namespace BossrScraper.Models
{
    // Represents a row in the killstats from tibia.com, example:
    // Race     Killed Players  Killed by Players   Killed Players	Killed by Players
    // Achad 	0               1 	                0 	            11
    public class KillStat
    {
        public string Name { get; set; }
        public int LastDayDeaths { get; set; }
        public int LastDayKills { get; set; }
        public int LastWeekDeaths { get; set; }
        public int LastWeekKills { get; set; }
    }
}
