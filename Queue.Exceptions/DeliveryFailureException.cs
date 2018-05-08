using System;

namespace Queue.Exceptions
{
    public class DeliveryFailureException : Exception
    {
        public DeliveryFailureException(string message, Exception ex)
            : base(message, ex)
        {
        }

        public DeliveryFailureException(string message)
            : base(message)
        {
        }
    }
}
