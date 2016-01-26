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
    public class CmdApplicationStoreWriter<T> : IStoreWriter<T> where T : IMessage
    {
        public Stream OpenStreamFor(T message)
        {
            throw new NotImplementedException();
        }
    }
}
