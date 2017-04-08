using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLife
{
    public sealed class Season : IEquatable<Season>
    {
        public string Comment { get; }

        public List<Episode> Episodes { get; }

        public Season(string comment, IEnumerable<Episode> episodes)
        {
            Comment = comment;
            Episodes = new List<Episode>(episodes);
        }

        public override string ToString()
        {
            return Comment;
        }

        public bool Equals(Season other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(other, this)) return true;
            return this.Comment == other.Comment
                && Enumerable.SequenceEqual(this.Episodes, other.Episodes);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Season);
        }

        public override int GetHashCode()
        {
            return Comment.GetHashCode() << 16
                | Episodes.GetHashCode() << 0;
        }

        public static bool operator ==(Season lhs, Season rhs) => lhs.Equals(rhs);

        public static bool operator !=(Season lhs, Season rhs) => !lhs.Equals(rhs);
    }
}
