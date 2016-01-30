using CommandLineApplicationLauncherPersistenceModel;
using System.IO;
using CommandLineApplicationLauncherModel;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class FileStoreWriter : IStoreWriter<SaveCmdApplicationConfigurationCommand>
    {
        public Stream OpenStreamFor(SaveCmdApplicationConfigurationCommand message)
        {
            return File. OpenWrite(string.Format("{0}.{1}", message.MessageId, "json"));
        }
    }
}
