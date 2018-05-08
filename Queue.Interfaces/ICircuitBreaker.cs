using System;

namespace Queue.Interfaces
{
    public interface ICircuitBreaker
    {
        void Execute(Action action);
        T Execute<T>(Func<T> action);
    }
}
