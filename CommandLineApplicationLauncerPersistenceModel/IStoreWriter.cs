using CommandLineApplicationLauncherModel;
using System.IO;

namespace CommandLineApplicationLauncherPersistenceModel
{
    public interface IStoreWriter<T> where T : IMessage
    {
        Stream OpenStreamFor(T message);
    }
}