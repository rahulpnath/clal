using CommandLineApplicationLauncherPersistenceModel;
using System.IO;
using CommandLineApplicationLauncherModel;
using System;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class FileNameStore : IStoreWriter<SaveCmdApplicationConfigurationCommand>, IStoreReader<string>
    {
        public bool CheckIfFileExists(string item)
        {
            throw new NotImplementedException();
        }

        public Stream OpenStreamFor(SaveCmdApplicationConfigurationCommand message)
        {
            return File. OpenWrite(string.Format("{0}.{1}", message.MessageId, "json"));
        }
    }
}
