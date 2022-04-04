using System.IO;
using System.Threading.Tasks;

namespace BackupsExtra.Entities.Logger.Console
{
    public sealed class ConsoleService : ILogger
        {
            public void WriteLog(string message)
            {
                System.Console.WriteLine(message);
            }
        }
    }