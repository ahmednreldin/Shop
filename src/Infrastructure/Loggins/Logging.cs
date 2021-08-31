using System;
using Microsoft.Extensions.Logging;
using Application.Loggins;

namespace Infrastructure.Loggins
{
    public class Logging : ILogging
    {
        private readonly ILogger<Logging> logger;

        public Logging(ILogger<Logging> logger)
        {
            this.logger = logger;
        }

        public void LogCritical(Exception exception) => 
            this.logger.LogCritical(exception,exception.Message);

        public void LogDebug(string message) =>
            this.logger.LogDebug(message);

        public void LogError(Exception exception) =>
            this.logger.LogError(exception,exception.Message);

        public void LogInformation(string message) =>
            this.logger.LogInformation(message);

        public void LogWarning(string message) =>
            this.logger.LogWarning(message);
    }
}
