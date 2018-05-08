using System;

namespace Queue.Exceptions
{
    public class QueueNotTransactionalException : Exception
    {
        public QueueNotTransactionalException(string message, Exception ex)
            : base(message, ex)
        {
        }

        public QueueNotTransactionalException(string message)
            : base(message)
        {
        }
    }
}
