using Contracts;
using Microsoft.Extensions.Logging;

namespace LoggerService
{
    public class AppLogger<TType> : IAppLogger
    {
        private readonly ILogger<TType> _logger;
        public AppLogger(ILogger<TType> logger)
        {
            _logger = logger;
        }

        public void LogDebug(string message) =>
            _logger.LogDebug(message);

        public void LogError(string message) =>
            _logger.LogError(message);

        public void LogInfo(string message) =>
            _logger.LogInformation(message);

        public void LogWarn(string message) => 
            _logger.LogWarning(message);
    }
}
