using CommandLineApplicationLauncherPersistenceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommandLineApplicationLauncherModel;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class CmdApplicationStoreWriter : IStoreWriter<SaveCmdApplicationConfigurationCommand>
    {
        public Stream OpenStreamFor(SaveCmdApplicationConfigurationCommand message)
        {
            return File.OpenWrite(string.Format("{0}.{1}", message.MessageId, "json"));
        }
    }
}
