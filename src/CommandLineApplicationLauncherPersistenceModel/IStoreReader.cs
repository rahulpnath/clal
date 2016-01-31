
namespace CommandLineApplicationLauncherPersistenceModel
{
    public interface IStoreReader<T>
    {
        bool CheckIfFileExists(T item);
    }
}
