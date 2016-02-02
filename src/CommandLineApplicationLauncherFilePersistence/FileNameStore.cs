using CommandLineApplicationLauncherPersistenceModel;
using System.IO;
using CommandLineApplicationLauncherModel;
using System;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class FileNameStore : IStoreWriter<string>, IStoreReader<string>
    {
        private const string fileExtension  = "json";

        public bool CheckIfFileExists(string item)
        {
            var fileName = Path.ChangeExtension(item, fileExtension);
            return File.Exists(fileName);
        }

        public Stream OpenStreamFor(string item)
        {
            var fileName = Path.ChangeExtension(item, fileExtension);
            return File.OpenWrite(fileName);
        }
    }
}
