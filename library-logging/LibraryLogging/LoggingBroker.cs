using Serilog;

namespace LibraryLogging
{
    public static class LoggingBroker
    {
        public static void LogDebug(string message) => Log.Debug(message);
        public static void LogInformation(string message) => Log.Information(message);
        public static void LogError(string message) => Log.Error(message);
    }
}
