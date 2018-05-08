using System;

namespace Queue.Exceptions
{
    public class BrokenCircuitException : Exception
    {
        public BrokenCircuitException(string message, Exception ex)
            : base(message, ex)
        {
        }

        public BrokenCircuitException(string message)
            : base(message)
        {
        }
    }
}
