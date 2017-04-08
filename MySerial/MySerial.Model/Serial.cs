using System;
using System.Threading.Tasks;

namespace MySerial.Model
{
    public class Serial
    {
        public Serial(string title, string description, ISerialSource source)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException();

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException();

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Title = title;
            Description = description;
            Source = source;
        }

        public string Title { get; }

        public string Description { get; }

        public ISerialSource Source { get; set; }

        public Task<SerialPlaylist> Load()
        {
            return Source.Load();
        }
    }
}
