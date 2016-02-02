using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherPersistenceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class CmdApplicationMetaStore : IStoreReader<CmdApplicationMeta>
    {
        private const string fileExtension = "json";
        private const string rootDirectory = "configs";

        public bool CheckIfFileExists(CmdApplicationMeta item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stream> OpenStreamsFor(CmdApplicationMeta item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var directory = item.GetConfigurationDirectoryInfo(rootDirectory);
            var searchPattern = string.Format("*.{0}", fileExtension);

            return from file in directory.GetFiles(searchPattern)
                   orderby file.CreationTime
                   select file.OpenRead();
        }
    }
}
