using System.Collections.Generic;
using BossrCoreAPI.Models.Interfaces;

namespace BossrCoreAPI.Models
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Creature> Creatures { get; set; }
    }
}
