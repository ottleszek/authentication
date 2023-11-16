namespace LibraryLogging
{
    public interface ILoggingBroker
    {
        public void LogDebug(string message);
        public void LogInformation(string message);
        public void LogError(string message);
    }
}
