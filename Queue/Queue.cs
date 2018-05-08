using System;
using System.Messaging;
using Queue.Exceptions;
using Queue.Interfaces;

namespace Queue
{
    public abstract class Queue : IPoisonQueue, IMainQueue
    {
        private readonly IMessageFormatter _messageFormatter;
        private readonly IMessageQueueFactory _messageQueueFactory;
        private readonly ICircuitBreaker _circuitBreaker;
        private readonly ILogger _logger;
        private readonly IQueuePathProvider _queuePathProvider;
        private MessageQueue _messageQueue;


        protected Queue(
            IQueuePathProvider queuePathProvider,
            IMessageFormatter messageFormatter,
            IMessageQueueFactory messageQueueFactory,
            ICircuitBreaker circuitBreaker,
            ILogger logger)
        {
            _queuePathProvider = queuePathProvider;
            _messageFormatter = messageFormatter;
            _messageQueueFactory = messageQueueFactory;
            _circuitBreaker = circuitBreaker;
            _logger = logger;
        }

        public void Initialize()
        {
            _messageQueue = _messageQueueFactory.Get(
                _queuePathProvider.Path,
                _messageFormatter);
        }

        public void Process<T>(
            Action<T> processMessage,
            IPoisonQueue poisonQueue) where T : class
        {
            if (processMessage == null)
                throw new ArgumentNullException(nameof(processMessage));

            if (poisonQueue == null)
                throw new ArgumentNullException(nameof(poisonQueue));

            if (_messageQueue == null)
                Initialize();

            try
            {
                //Sanity Count
                var processingMessageCount = 1000;
                var messageEnumerator = _messageQueue.GetMessageEnumerator2();

                while (messageEnumerator.MoveNext(TimeSpan.FromSeconds(1))
                    && processingMessageCount > 0)
                {
                    processingMessageCount--;

                    if (messageEnumerator.Current == null)
                        continue;

                    using (var mainTransaction = new MessageQueueTransaction())
                    using (var posionTransaction = new MessageQueueTransaction())
                    {
                        try
                        {
                            mainTransaction.Begin();
                            posionTransaction.Begin();

                            Message message = null;

                            try
                            {
                                message = _messageQueue.ReceiveById(messageEnumerator.Current.Id, mainTransaction);
                            }
                            catch
                            {
                                // ignored 
                            }

                            //message is probably moved to another queue
                            if (message == null)
                            {
                                mainTransaction.Abort();
                                posionTransaction.Abort();
                                continue;
                            }

                            poisonQueue.Send(message, posionTransaction);

                            var obj = (T)message.Body;
                            _circuitBreaker.Execute(() => processMessage(obj));

                            mainTransaction.Commit();
                            posionTransaction.Abort();
                        }
                        catch (DeliveryFailureException ex)
                        {
                            mainTransaction.Abort();
                            posionTransaction.Abort();
                            _logger.Error("Delivery error", ex);
                        }
                        catch (BrokenCircuitException ex)
                        {
                            mainTransaction.Abort();
                            posionTransaction.Abort();
                            _logger.Error("Broken circuit", ex);
                            break;
                        }
                        catch (Exception ex)
                        {
                            posionTransaction.Commit();
                            mainTransaction.Commit();
                            _logger.Error("Processing error", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new QueueException("Error handling message from queue", ex);
            }
        }

        public void Send<T>(T obj) where T : class
        {
            try
            {
                if (_messageQueue == null)
                    Initialize();

                using (var transaction = new MessageQueueTransaction())
                {
                    transaction.Begin();

                    var message = new Message(obj, _messageFormatter);
                    _messageQueue.Send(message, transaction);

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new QueueException("Unable to send message", ex);
            }
        }

        public void Send(Message message, MessageQueueTransaction transaction)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            try
            {
                if (_messageQueue == null)
                    Initialize();

                _messageQueue.Send(message, transaction);
            }
            catch (Exception ex)
            {
                throw new QueueException("Unable to send message", ex);
            }
        }
    }

}
