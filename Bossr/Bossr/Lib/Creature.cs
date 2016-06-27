namespace Bossr.Lib
{
    public class Creature
    {
        public Category Category { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Monitored { get; set; }
        public int HoursBetweenEachSpawnMin { get; set; }
        public int HoursBetweenEachSpawnMax { get; set; }
        public string CategoryId { get; set; }
    }
}