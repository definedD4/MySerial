using MySerial.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace MySerial.Repository.LocalRepository
{
    public class SerialDescriptor
    {
        private SerialDescriptor(string title, string description, SerialPlaylist playlist)
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
                    (string)s["title"],
                    new LocalMediaSource((string)e["media"])
                )
                ))));

            return new SerialDescriptor(title, description, playlist);
        }
    }
}