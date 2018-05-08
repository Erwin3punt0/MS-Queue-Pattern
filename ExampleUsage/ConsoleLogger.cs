using System;
using Queue.Interfaces;

namespace ExampleUsage
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine($"Info: {message}");
        }

        public void Warning(string message)
        {
            Console.WriteLine($"Warning: {message}");
        }

        public void Error(string message, Exception ex)
        {
            Console.WriteLine($"Error: {message}");
        }
    }
}
