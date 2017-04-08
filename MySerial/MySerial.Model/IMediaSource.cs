using System;

namespace MySerial.Model
{
    public interface IMediaSource
    {
        Uri PlayUri { get; }

        Uri DownloadUri { get; }
    }
}