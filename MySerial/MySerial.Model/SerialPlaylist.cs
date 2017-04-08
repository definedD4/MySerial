using System;
using System.Collections.Generic;

namespace MySerial.Model
{
    public class SerialPlaylist
    {
        public SerialPlaylist(IEnumerable<Season> seasons)
        {
            if (seasons == null)
                throw new ArgumentNullException(nameof(seasons));

            Seasons = new List<Season>(seasons);
        }

        public IReadOnlyList<Season> Seasons { get; }
    }
}