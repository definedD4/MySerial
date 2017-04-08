using MySerial.Model;
using System;

namespace MySerial.Repository.LocalRepository
{
    public class LocalMediaSource : IMediaSource
    {
        public string Path { get; }

        public LocalMediaSource(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Path = path;
        }
    }
}