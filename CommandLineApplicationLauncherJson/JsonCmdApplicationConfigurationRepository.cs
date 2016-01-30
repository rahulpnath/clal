using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherPersistenceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherJson
{
    public class JsonCmdApplicationConfigurationRepository : ICmdApplicationConfigurationRepository
    {
        public IStoreWriter<string> FileStoreWriter { get; private set; }

        public JsonCmdApplicationConfigurationRepository(IStoreWriter<string> fileStoreWriter)
        {
            if (fileStoreWriter == null)
                throw new ArgumentNullException(nameof(fileStoreWriter));

            this.FileStoreWriter = fileStoreWriter;
        }

        public bool CheckIfConfigurationWithSameNameExists(CmdApplicationConfiguration application)
        {
            throw new NotImplementedException();
        }
    }
}
