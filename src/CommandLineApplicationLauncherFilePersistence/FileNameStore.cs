using CommandLineApplicationLauncherPersistenceModel;
using System.IO;
using CommandLineApplicationLauncherModel;
using System;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class FileNameStore : IStoreWriter<CmdApplicationConfiguration>, IStoreReader<CmdApplicationConfiguration>
    {
        private const string fileExtension  = "json";

        public bool CheckIfFileExists(CmdApplicationConfiguration item)
        {
            var fileName = item.GetFileName(fileExtension);
            return File.Exists(fileName);
        }

        public Stream OpenStreamFor(CmdApplicationConfiguration item)
        {
            var fileName = item.GetFileName(fileExtension);
            return File.OpenWrite(fileName);
        }

        
    }
}
