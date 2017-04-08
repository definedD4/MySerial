using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OnlineLife
{
    public sealed class Playlist
    {
        public List<Season> Seasons { get; }

        public Playlist(IEnumerable<Season> seasons)
        {
            Seasons = new List<Season>(seasons);
        }

        public static Playlist FromJSON(string json)
        {
            return new Playlist(
                JObject.Parse(json)["playlist"].Select(
                    s => new Season(
                        (string)s["comment"],
                        s["playlist"].Select(
                            e => new Episode(
                                (string)e["comment"],
                                new Uri((string)e["file"]),
                                new Uri((string)e["download"]))))));
        }

        public static async Task<Playlist> DownloadFromAsync(Uri playlist)
        {
            var client = new WebClient();

            string json = await client.DownloadStringTaskAsync(playlist);

            return FromJSON(json);
        }

        public bool Equals(Playlist other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(other, this)) return true;
            return this.Seasons.SequenceEqual(other.Seasons);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Playlist);
        }

        public override int GetHashCode()
        {
            return Seasons.GetHashCode();
        }

        public static bool operator ==(Playlist lhs, Playlist rhs) => lhs.Equals(rhs);

        public static bool operator !=(Playlist lhs, Playlist rhs) => !lhs.Equals(rhs);
    }
}
