using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class SaveCmdApplicationConfigurationCommand : IMessage
    {
        public Guid MessageId { get; private set; }
    }
}
