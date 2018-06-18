
namespace UKLON.TestTask.IntegrationAdapter
{
    public interface IFileWorker
    {
        void WriteToFile<Data,Result>(Data data, Result result) where Result : IResponse;
    }
}
