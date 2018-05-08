using System.Messaging;

namespace Queue.Interfaces
{
    public interface IMessageQueueFactory
    {
        MessageQueue Get(string queuePath, IMessageFormatter messageFormatter);
    }
}
