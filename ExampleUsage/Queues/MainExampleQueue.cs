using System.Messaging;
using ExampleUsage.Dto;
using ExampleUsage.Paths;
using Queue.Interfaces;

namespace ExampleUsage.Queues
{
    public class MainExampleQueue : Queue.Queue
    {
        public MainExampleQueue(
            IMessageQueueFactory messageQueueFactory,
            ICircuitBreaker circuitBreaker,
            ILogger logger)
            : base(
                new MainExampleQueuePath(),
                new XmlMessageFormatter(new[] { typeof(SerializableMessage) }),
                messageQueueFactory,
                circuitBreaker,
                logger)
        {
        }
    }
}
