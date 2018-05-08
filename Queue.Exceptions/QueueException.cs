using System;

namespace Queue.Exceptions
{
    public class QueueException : Exception
    {
        public QueueException(string message, Exception ex)
            : base(message, ex)
        {
        }

        public QueueException(string message)
            : base(message)
        {
        }
    }
}
