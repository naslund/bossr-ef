using System.Collections.Generic;
using BossrLib.Interfaces;
using Newtonsoft.Json;

namespace BossrLib.Classes
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorRgbHex { get; set; }

        [JsonIgnore]
        public List<Creature> Creatures { get; set; }
    }
}
