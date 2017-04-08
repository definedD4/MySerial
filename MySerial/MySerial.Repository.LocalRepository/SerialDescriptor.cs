using MySerial.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace MySerial.Repository.LocalRepository
{
    public sealed class SerialDescriptor : IEquatable<SerialDescriptor>
    {
        public SerialDescriptor(string title, string description, SerialPlaylist playlist)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException();

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException();

            if (playlist == null)
                throw new ArgumentNullException(nameof(playlist));

            Title = title;
            Description = description;
            Playlist = playlist;
        }

        public string Title { get; }

        public string Description { get; }

        public SerialPlaylist Playlist { get; }

        public static SerialDescriptor Parse(string text)
        {
            var json = JObject.Parse(text);

            string title = (string)json["title"];
            string description = (string)json["description"];
            var playlist = new SerialPlaylist(json["playlist"].Select(s => new Season(
                (string)s["title"],
                s["episodes"].Select(e => new Episode(
                    (string)e["title"],
                    new LocalMediaSource((string)e["media"])
                )
                ))));

            return new SerialDescriptor(title, description, playlist);
        }

        public string ToJson()
        {
            var root = JObject.FromObject(new
            {
                title = Title,
                description = Description,
                playlist = from season in Playlist.Seasons
                           select new
                           {
                               title = season.Title,
                               episodes = from episode in season.Episodes
                                          select new
                                          {
                                              title = episode.Title,
                                              media = (episode.Media as LocalMediaSource)?.Path
                                          }
                           }
            });

            return root.ToString();
        }

        public bool Equals(SerialDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Title, other.Title) && string.Equals(Description, other.Description) && Playlist.Equals(other.Playlist);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is SerialDescriptor && Equals((SerialDescriptor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Title.GetHashCode();
                hashCode = (hashCode * 397) ^ Description.GetHashCode();
                hashCode = (hashCode * 397) ^ Playlist.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(SerialDescriptor left, SerialDescriptor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SerialDescriptor left, SerialDescriptor right)
        {
            return !Equals(left, right);
        }
    }
}