using CommandLineApplicationLauncherPersistenceModel;
using System.IO;
using CommandLineApplicationLauncherModel;
using System;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class FileNameStore : IStoreWriter<string>, IStoreReader<string>
    {
        public bool CheckIfFileExists(string item)
        {
            return File.Exists(item);
        }

        public Stream OpenStreamFor(string item)
        {
            return File.OpenWrite(item);
        }
    }
}
