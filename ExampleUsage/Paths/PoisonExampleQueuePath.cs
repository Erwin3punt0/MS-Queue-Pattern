using Queue.Interfaces;

namespace ExampleUsage.Paths
{
    public class PoisonExampleQueuePath : IQueuePathProvider
    {
        public string Path { get; } = ".\\private$\\Poison_ExampleQueue";
    }
}
