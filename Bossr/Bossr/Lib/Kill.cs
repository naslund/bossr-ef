using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using Xamarin.Forms;

namespace Bossr.Lib
{
    public class Kill
    {
        public Creature Creature { get; set; }
        public Guid Id { get; set; }
        public DateTime SpawnedAtMin { get; set; }
        public DateTime SpawnedAtMax { get; set; }
        public Guid? SpawnPointId { get; set; }
        public Guid CreatureId { get; set; }
        public Guid WorldId { get; set; }

        public Color CategoryColor => Color.FromRgb(Creature.Category.ColorR, Creature.Category.ColorG, Creature.Category.ColorB);

        public string TimeAgo => $"{(DateTime.SpecifyKind(SpawnedAtMin, DateTimeKind.Utc) - DateTime.UtcNow).Humanize(2, CultureInfo.InvariantCulture)} ago";
    }
}
