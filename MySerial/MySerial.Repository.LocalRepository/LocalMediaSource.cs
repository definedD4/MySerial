using MySerial.Model;
using System;

namespace MySerial.Repository.LocalRepository
{
    public class LocalMediaSource : IMediaSource, IEquatable<LocalMediaSource>
    {
        public string Path { get; }

        public LocalMediaSource(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Path = path;
        }

        public Uri PlayUri => new Uri(Path);

        public Uri DownloadUri => new Uri(Path);

        public bool Equals(LocalMediaSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Path, other.Path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LocalMediaSource) obj);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        public static bool operator ==(LocalMediaSource left, LocalMediaSource right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LocalMediaSource left, LocalMediaSource right)
        {
            return !Equals(left, right);
        }
    }
}