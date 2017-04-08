using MySerial.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerial.Repository.LocalRepository
{
    public class LocalSerial : ISerialSource
    {
        private const string SerialDescriptorFile = "serial.json";

        private LocalSerial(string title, string description, SerialPlaylist playlist)
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

        public Task<SerialPlaylist> Load()
        {
            return Task.FromResult(Playlist);
        }

        public static LocalSerial LoadFrom(string path)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Specified directory does not exist.");

            string serialDescriptorPath = path + "/" + SerialDescriptorFile;

            if (!File.Exists(serialDescriptorPath))
                throw new ArgumentException($"Specified directory does not contain serial descriptor file ({SerialDescriptorFile}).");

            string descriptorText = File.ReadAllText(serialDescriptorPath);

            var descriptor = SerialDescriptor.Parse(descriptorText);

            return new LocalSerial(descriptor.Title, descriptor.Description, descriptor.Playlist);
        }

        public static void SaveDescriptor(string directoryPath, SerialDescriptor descriptor)
        {
            if (!Directory.Exists(directoryPath))
                throw new ArgumentException("Specified directory does not exist.");

            string descriptorPath = directoryPath + "/" + SerialDescriptorFile;

            File.WriteAllText(descriptorPath, descriptor.ToJson());
        }
    }
}
