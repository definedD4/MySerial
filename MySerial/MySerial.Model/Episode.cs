using System;

namespace MySerial.Model
{
    public class Episode
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
    }
}