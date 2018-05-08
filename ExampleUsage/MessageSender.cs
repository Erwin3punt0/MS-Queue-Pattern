using ExampleUsage.Dto;
using Queue.Interfaces;

namespace ExampleUsage
{
    public class MessageSender
    {
        private readonly IMainQueue _mainQueue;

        public MessageSender(IMainQueue mainQueue)
        {
            _mainQueue = mainQueue;
        }

        public void SendSomeStuff()
        {
            _mainQueue.Send(new SerializableMessage { Body = "Some message" });
        }
    }
}
