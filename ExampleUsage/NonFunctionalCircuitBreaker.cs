using System;
using Queue.Interfaces;

namespace ExampleUsage
{
    public class NonFunctionalCircuitBreaker : ICircuitBreaker
    {
        public void Execute(Action action)
        {
            action();
        }

        public T Execute<T>(Func<T> action)
        {
            return action();
        }
    }
}
