using System.Messaging;

namespace Queue.Interfaces
{
    public interface IPoisonQueue : IQueue
    {
        void Send(Message message, MessageQueueTransaction transaction);
    }
}
