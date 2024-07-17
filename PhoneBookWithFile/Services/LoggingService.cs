namespace PhoneBookWithFile.Services
{
    internal class LoggingService : ILoggingService
    {
        public LoggingService()
        {
        }

        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
    }
}