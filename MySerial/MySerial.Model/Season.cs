using System;
using System.Collections.Generic;

namespace MySerial.Model
{
    public class Season
    {
        public Season(string title, IEnumerable<Episode> episodes)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException();

            if (episodes == null)
                throw new ArgumentNullException(nameof(episodes));

            Title = title;
            Episodes = new List<Episode>(episodes);
        }

        public string Title { get; }

        public IReadOnlyList<Episode> Episodes { get; }
    }
}