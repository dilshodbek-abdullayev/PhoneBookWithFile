namespace PhoneBookWithFile.Services
{
    internal interface ILoggingService
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}