using System.Messaging;
using ExampleUsage.Dto;
using ExampleUsage.Paths;
using Queue.Interfaces;

namespace ExampleUsage.Queues
{
    public class PoisonExampleQueue : Queue.Queue
    {
        public PoisonExampleQueue(
            IMessageQueueFactory messageQueueFactory,
            ICircuitBreaker circuitBreaker,
            ILogger logger)
            : base(
                new PoisonExampleQueuePath(),
                new XmlMessageFormatter(new[] { typeof(SerializableMessage) }),
                messageQueueFactory,
                circuitBreaker,
                logger)
        {
        }
    }
}
