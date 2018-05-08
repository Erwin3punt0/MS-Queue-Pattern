using System;
using System.Messaging;
using Queue.Exceptions;
using Queue.Interfaces;

namespace Queue
{
    public class MessageQueueFactory : IMessageQueueFactory
    {
        public MessageQueue Get(
            string queuePath,
            IMessageFormatter messageFormatter)
        {
            MessageQueue messageQueue;

            try
            {
                if (!MessageQueue.Exists(queuePath))
                    throw new QueueNotExistException($"Queue with path {queuePath} does not exist");

                messageQueue = new MessageQueue(queuePath)
                {
                    Formatter = messageFormatter
                };

                if (!messageQueue.Transactional)
                    throw new QueueNotTransactionalException($"Queue {queuePath} is non transactional.");
            }
            catch (Exception ex)
            {
                throw new QueueException($"Unable to connect to queue {queuePath}.", ex);
            }

            return messageQueue;
        }
    }
}
