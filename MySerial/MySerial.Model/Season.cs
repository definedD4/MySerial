using System;
using System.Collections.Generic;
using System.Linq;

namespace MySerial.Model
{
    public sealed class Season : IEquatable<Season>
    {
        public Season(string title, IEnumerable<Episode> episodes)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException();

            if (episodes == null)
                throw new ArgumentNullException(nameof(episodes));

            Title = title;
            Episodes = new List<Episode>(episodes);
        }

        public string Title { get; }

        public IReadOnlyList<Episode> Episodes { get; }

        public bool Equals(Season other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Title, other.Title) && Episodes.SequenceEqual(other.Episodes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Season && Equals((Season) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Title.GetHashCode() * 397) ^ Episodes.GetHashCode();
            }
        }

        public static bool operator ==(Season left, Season right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Season left, Season right)
        {
            return !Equals(left, right);
        }
    }
}