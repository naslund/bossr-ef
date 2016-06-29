using System;
using Xamarin.Forms;

namespace Bossr.Lib
{
    public class Category : IEquatable<Category>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }

        public Color Color => Color.FromRgb(ColorR, ColorG, ColorB);

        public bool Equals(Category other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}