using ExampleUsage.Dto;
using ExampleUsage.Queues;
using Queue;
using Queue.Interfaces;

namespace ExampleUsage
{
    public class MessageSender
    {
        private readonly IMainQueue _mainQueue;

        public MessageSender()
        {
            _mainQueue = new MainExampleQueue(
                new MessageQueueFactory(),
                new NonFunctionalCircuitBreaker(),
                new ConsoleLogger());
        }

        public void SendSomeStuff()
        {
            _mainQueue.Send(new SerializableMessage { Body = "Some message" });
        }
    }
}
