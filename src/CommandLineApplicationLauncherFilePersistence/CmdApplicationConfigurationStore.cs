using CommandLineApplicationLauncherPersistenceModel;
using System.IO;
using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class CmdApplicationConfigurationStore : IStoreWriter<CmdApplicationConfiguration>, IStoreReader<CmdApplicationConfiguration>
    {
        private const string fileExtension  = "json";
        private const string rootDirectory = "configs";

        public bool CheckIfFileExists(CmdApplicationConfiguration item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var filePath = item.GetFilePath(fileExtension, rootDirectory);
            return File.Exists(filePath);
        }

        public Stream OpenStreamFor(CmdApplicationConfiguration item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var filePath = item.GetFilePath(fileExtension, rootDirectory);
            return File.OpenWrite(filePath);
        }

        public IEnumerable<Stream> OpenStreamsFor(CmdApplicationConfiguration item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var directory = item.GetDirectoryInfo(rootDirectory);
            var searchPattern = string.Format("*.{1}", fileExtension);

            return from file in directory.GetFiles(searchPattern)
            orderby file.CreationTime descending
            select file.OpenRead();
        }
    }
}
