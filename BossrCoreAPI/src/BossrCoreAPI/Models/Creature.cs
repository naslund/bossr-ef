using BossrCoreAPI.Models.Interfaces;

namespace BossrCoreAPI.Models
{
    public class Creature : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}