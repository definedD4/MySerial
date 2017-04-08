using MySerial.Model;
using MySerial.Repository.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerial.Repository.LocalRepository
{
    public class LocalRepository : ISerialRepository
    {
        private string m_Path;

        public LocalRepository(string path, string name)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (!Directory.Exists(path))
                throw new ArgumentException("Specified directory doesn not exist.");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();

            m_Path = path;
            Name = name;
        }

        public string Name { get; }

        public async Task<IEnumerable<Serial>> GetSerials(string filter = null)
        {
            var dirs = Directory.GetDirectories(m_Path);

            var serials = new List<Serial>();
            foreach (var dir in dirs)
            {
                try
                {
                    var serial = LocalSerial.LoadFrom(dir);
                    serials.Add(new Serial(serial.Title, serial.Description, serial));
                }
                catch
                {

                }
            }

            return serials;
        }
    }
}
