using System.Collections.Generic;
using BossrLib.Interfaces;

namespace BossrLib.Classes
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Creature> Creatures { get; set; }
    }
}
