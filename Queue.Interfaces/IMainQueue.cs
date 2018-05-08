using System;

namespace Queue.Interfaces
{
    public interface IMainQueue : IQueue
    {
        void Process<T>(Action<T> processMessage, IPoisonQueue poisonQueue) where T : class;
        void Send<T>(T obj) where T : class;
    }
}
