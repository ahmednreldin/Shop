using System;

namespace Application.Loggins
{
    public interface ILogging
    {
        void LogInformation(string message);
        void LogDebug(string message);
        void LogWarning(string message);
        void LogError(Exception exception);
        void LogCritical(Exception exception);
    }
}
