using MySerial.Repository.LocalRepository;
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
            var repo = new LocalRepository("repo/", "Local repository");

            var serials = repo.GetSerials().Result;

            foreach (var serial in serials)
            {
                Console.WriteLine($"Name: {serial.Title}");
                Console.WriteLine($"Description: {serial.Description}");
                Console.WriteLine("Seasons:");
                foreach (var season in serial.Load().Result.Seasons)
                {
                    Console.WriteLine($"\tTitle:{season.Title}");
                    Console.WriteLine($"\tEpisodes:{season.Title}");
                    foreach (var episode in season.Episodes)
                    {
                        Console.WriteLine($"\t\tTitle: {episode.Title}");
                        Console.WriteLine($"\t\tMedia: {(episode.Media as LocalMediaSource)?.Path}");
                    }
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
