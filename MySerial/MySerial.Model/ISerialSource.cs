using System.Threading.Tasks;

namespace MySerial.Model
{
    public interface ISerialSource
    {
        Task<SerialPlaylist> Load();
    }
}
