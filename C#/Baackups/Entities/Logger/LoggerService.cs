using System;
using BackupsExtra.Entities.Logger.Console;
using BackupsExtra.Entities.Logger.File;

namespace BackupsExtra.Entities.Logger
{
    public sealed class LoggerService<T> : ILoggerService<T>
    {
        private readonly ILogger _fileService;

        private readonly string _nameSpace;

        private readonly string _currentTime = DateTime.Now.ToLongTimeString();

        public LoggerService(ILogger fileService)
        {
            _fileService = fileService;
            _nameSpace = typeof(T).FullName;
        }

        public void Log(LogLevel level, string message, short traceId = 0, Exception exception = null)
        {
            string response = CastMessage(message, level, traceId, exception);
            switch (level)
            {
                case LogLevel.Information:
                    LogInformation(response);
                    break;
                case LogLevel.Warning:
                    LogWarning(response);
                    break;
                case LogLevel.Error:
                    LogError(response);
                    break;
                case LogLevel.Critical:
                    LogCritical(response);
                    break;
                case LogLevel.Trace:
                    break;
                case LogLevel.Debug:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public void Trace(string message)
        {
            _fileService.WriteLog(message);
        }

        public void LogInformation(string message)
        {
            _fileService.WriteLog(message);
        }

        public void LogWarning(string message)
        {
            _fileService.WriteLog(message);
        }

        public void LogError(string message)
        {
            _fileService.WriteLog(message);
        }

        public void LogCritical(string message)
        {
            _fileService.WriteLog(message);
        }

        private string CastMessage(string message, LogLevel level, short traceId, Exception exception)
        {
            return exception != null
                ? $"{_currentTime}|{traceId}|{_nameSpace}|{level.ToString()}|{message}{exception}"
                : $"{_currentTime}|{traceId}|{_nameSpace}|{level.ToString()}|{message}";
        }
    }
}