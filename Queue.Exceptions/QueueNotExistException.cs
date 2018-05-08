using System;

namespace Queue.Exceptions
{
    public class QueueNotExistException : Exception
    {
        public QueueNotExistException(string message, Exception ex)
            : base(message, ex)
        {
        }

        public QueueNotExistException(string message)
            : base(message)
        {
        }
    }
}
