using ExampleUsage.Dto;
using Queue.Interfaces;

namespace ExampleUsage
{
    public class MessageProcessor
    {
        private readonly IMainQueue _mainQueue;
        private readonly IPoisonQueue _poisonQueue;

        public MessageProcessor(
            IMainQueue mainQueue,
            IPoisonQueue poisonQueue)
        {
            _mainQueue = mainQueue;
            _poisonQueue = poisonQueue;
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
