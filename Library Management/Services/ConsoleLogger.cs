using Library_Management.Services.Interfaces;

namespace Library_Management.Services
{
    public class ConsoleLogger : ILog
    {
        public void Info(string textToLog)
        {
            Console.WriteLine(textToLog);
        }
    }
}