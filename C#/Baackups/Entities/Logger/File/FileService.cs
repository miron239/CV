using System;
using System.IO;
using System.Threading.Tasks;

namespace BackupsExtra.Entities.Logger.File
{
    public sealed class FileService : ILogger
    {
        private const string Path = @"/Users/mironbabicev/RiderProjects/miron239/BackupsExtra/LOGGER";

        public async void WriteLog(string message)
        {
            await using var sw = new StreamWriter(Path, true, System.Text.Encoding.Default);
            await sw.WriteLineAsync(message);
        }

        public async Task ReadLogs()
        {
            using var sr = new StreamReader(Path);
            System.Console.WriteLine(await sr.ReadToEndAsync());
        }
    }
}