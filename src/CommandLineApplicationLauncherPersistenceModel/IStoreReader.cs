
using CommandLineApplicationLauncherModel;
using System.Collections.Generic;
using System.IO;

namespace CommandLineApplicationLauncherPersistenceModel
{
    public interface IStoreReader<T>
    {
        bool CheckIfFileExists(T item);

        IEnumerable<Stream> OpenStreamsFor(T item);
    }
}
