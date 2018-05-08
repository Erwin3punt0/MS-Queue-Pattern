using ExampleUsage.Dto;
using ExampleUsage.Queues;
using Queue;
using Queue.Interfaces;

namespace ExampleUsage
{
    public class MessageProcessor
    {
        private readonly IMainQueue _mainQueue;
        private readonly IPoisonQueue _poisonQueue;

        public MessageProcessor()
        {
            _mainQueue = _mainQueue = new MainExampleQueue(
                new MessageQueueFactory(),
                new NonFunctionalCircuitBreaker(),
                new ConsoleLogger());

            _poisonQueue = new PoisonExampleQueue(
                new MessageQueueFactory(),
                new NonFunctionalCircuitBreaker(),
                new ConsoleLogger());
        }

        public void Process()
        {
            _mainQueue.Process<SerializableMessage>(ProcessTheMessage, _poisonQueue);
        }

        private void ProcessTheMessage(SerializableMessage serializableMessage)
        {
            //Process messages
        }
    }
}
