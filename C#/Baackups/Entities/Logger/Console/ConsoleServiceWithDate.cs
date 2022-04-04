using System;

namespace BackupsExtra.Entities.Logger.Console
{
    public class ConsoleServiceWithDate : ILogger
    {
        public void WriteLog(string message)
        {
            System.Console.WriteLine(DateTime.Now + message);
        }
    }
}