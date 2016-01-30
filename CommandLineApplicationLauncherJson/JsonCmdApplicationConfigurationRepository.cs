using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherPersistenceModel;
using System;

namespace CommandLineApplicationLauncherJson
{
    public class JsonCmdApplicationConfigurationRepository : ICmdApplicationConfigurationRepository
    {
        public IStoreWriter<string> FileStoreWriter { get; private set; }
        public IStoreReader<string> FileStoreReader { get; private set; }

        public JsonCmdApplicationConfigurationRepository(
            IStoreWriter<string> fileStoreWriter,
            IStoreReader<string> fileStoreReader)
        {
            if (fileStoreWriter == null)
                throw new ArgumentNullException(nameof(fileStoreWriter));

            if (fileStoreReader == null)
                throw new ArgumentNullException(nameof(fileStoreReader));

            this.FileStoreWriter = fileStoreWriter;
            this.FileStoreReader = fileStoreReader;
        }

        public bool CheckIfConfigurationWithSameNameExists(CmdApplicationConfiguration application)
        {
            throw new NotImplementedException();
        }
    }
}
