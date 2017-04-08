using OnlineLife;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter playlist URL: ");
            var playlistSource = new Uri(Console.ReadLine());

            try
            {
                var playlist = Playlist.DownloadFromAsync(playlistSource).Result;

                Console.WriteLine("\nDownload links:");

                var episodes = playlist.Seasons.SelectMany(s => s.Episodes);
                foreach (var episode in episodes)
                {
                    Console.WriteLine(episode.Download);
                }

                Console.Write("\nEnter download folder name: ");
                string folder = Console.ReadLine();

                Console.Write("\nSkip episodes: ");
                int skip = int.Parse(Console.ReadLine());

                var client = new WebClient();

                int progressRow = -1;
                int precentage = 0;
                long bytesRecieved = 0, totalBytes = 0;
                client.DownloadProgressChanged += (s, e) =>
                {
                    precentage = e.ProgressPercentage;
                    bytesRecieved = e.BytesReceived;
                    totalBytes = e.TotalBytesToReceive;
                };

                Directory.CreateDirectory(folder);

                foreach (var episode in episodes.Skip(skip))
                {
                    string fileName = episode.Download.GetLeftPart(UriPartial.Path).Split('/').Last();
                    Console.WriteLine($"Downloading {episode.Comment} to {fileName}");
                    progressRow = Console.CursorTop;
                    client.DownloadFileAsync(episode.Download, folder + "\\" + fileName);
                    while (client.IsBusy)
                    {
                        Thread.Sleep(250);
                        Console.SetCursorPosition(0, progressRow);
                        Console.WriteLine($"{precentage}%: {bytesRecieved}/{totalBytes}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown while processing playlist:");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
