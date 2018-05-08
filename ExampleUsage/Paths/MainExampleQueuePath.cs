using Queue.Interfaces;

namespace ExampleUsage.Paths
{
    public class MainExampleQueuePath : IQueuePathProvider
    {
        public string Path { get; } = ".\\private$\\Main_ExampleQueue";
    }
}
