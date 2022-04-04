using System.Threading.Tasks;

namespace BackupsExtra.Entities.Logger
{
    public interface ILogger
    {
        void WriteLog(string message);
    }
}