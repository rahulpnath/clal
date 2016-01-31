using CommandLineApplicationLauncherModel;
using System.IO;

namespace CommandLineApplicationLauncherPersistenceModel
{
    public interface IStoreWriter<T>
    {
        Stream OpenStreamFor(T item);
    }
}