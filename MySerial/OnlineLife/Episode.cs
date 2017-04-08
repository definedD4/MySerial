using System;

namespace OnlineLife
{
    public sealed class Episode : IEquatable<Episode>
    {
        public string Comment { get; }

        public Uri File { get; }

        public Uri Download { get; }

        public Episode(string comment, Uri file, Uri download)
        {
            Comment = comment;
            File = file;
            Download = download;
        }

        public override string ToString()
        {
            return Comment;
        }

        public bool Equals(Episode other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(other, this)) return true;
            return this.Comment == other.Comment
                && this.File == other.File
                && this.Download == other.Download;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Episode);
        }

        public override int GetHashCode()
        {
            return Comment.GetHashCode() << 16
                | File.GetHashCode() << 8
                | Download.GetHashCode() << 0;
        }

        public static bool operator ==(Episode lhs, Episode rhs) => lhs.Equals(rhs);

        public static bool operator !=(Episode lhs, Episode rhs) => !lhs.Equals(rhs);
    }
}