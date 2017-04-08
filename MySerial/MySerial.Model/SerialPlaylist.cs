using System;
using System.Collections.Generic;
using System.Linq;

namespace MySerial.Model
{
    public sealed class SerialPlaylist : IEquatable<SerialPlaylist>
    {
        public SerialPlaylist(IEnumerable<Season> seasons)
        {
            if (seasons == null)
                throw new ArgumentNullException(nameof(seasons));

            Seasons = new List<Season>(seasons);
        }

        public IReadOnlyList<Season> Seasons { get; }

        public bool Equals(SerialPlaylist other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Seasons.SequenceEqual(other.Seasons);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is SerialPlaylist && Equals((SerialPlaylist) obj);
        }

        public override int GetHashCode()
        {
            return Seasons.GetHashCode();
        }

        public static bool operator ==(SerialPlaylist left, SerialPlaylist right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SerialPlaylist left, SerialPlaylist right)
        {
            return !Equals(left, right);
        }
    }
}