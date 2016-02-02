using CommandLineApplicationLauncherPersistenceModel;
using System.IO;
using CommandLineApplicationLauncherModel;
using System;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class FileNameStore : IStoreWriter<CmdApplicationConfiguration>, IStoreReader<CmdApplicationConfiguration>
    {
        private const string fileExtension  = "json";
        private const string rootDirectory = "configs";

        public bool CheckIfFileExists(CmdApplicationConfiguration item)
        {
            var filePath = item.GetFilePath(fileExtension, rootDirectory);
            return File.Exists(filePath);
        }

        public Stream OpenStreamFor(CmdApplicationConfiguration item)
        {
            var filePath = item.GetFilePath(fileExtension, rootDirectory);
            return File.OpenWrite(filePath);
        }

        
    }
}
