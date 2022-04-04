using System;
using System.IO;
using System.Threading.Tasks;

namespace BackupsExtra.Entities.Logger.File
{
    public class FileServiceWithTime : ILogger
    {
        private const string Path = @"/Users/mironbabicev/RiderProjects/miron239/BackupsExtra/LOGGER";

        public async void WriteLog(string message)
        {
            await using var sw = new StreamWriter(Path, true, System.Text.Encoding.Default);
            await sw.WriteLineAsync(DateTime.Now + " " + message);
        }

        public async Task ReadLogs()
        {
            using var sr = new StreamReader(Path);
            System.Console.WriteLine(DateTime.Now + " " + await sr.ReadToEndAsync());
        }
    }
}