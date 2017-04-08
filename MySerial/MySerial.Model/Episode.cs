using System;

namespace MySerial.Model
{
    public sealed class Episode : IEquatable<Episode>
    {
        public Episode(string title, IMediaSource media)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException();

            if (media == null)
                throw new ArgumentNullException(nameof(media));

            Title = title;
            Media = media;
        }

        public string Title { get; }

        public IMediaSource Media { get; }

        public bool Equals(Episode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Title, other.Title) && Media.Equals(other.Media);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Episode && Equals((Episode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Title.GetHashCode() * 397) ^ Media.GetHashCode();
            }
        }

        public static bool operator ==(Episode left, Episode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Episode left, Episode right)
        {
            return !Equals(left, right);
        }
    }
}