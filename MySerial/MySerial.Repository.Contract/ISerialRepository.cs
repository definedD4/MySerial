using MySerial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerial.Repository.Contract
{
    public interface ISerialRepository
    {
        string Name { get; }

        Task<IEnumerable<Serial>> GetSerials(string filter = null);
    }
}
