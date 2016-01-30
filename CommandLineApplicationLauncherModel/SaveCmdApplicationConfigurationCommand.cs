using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class SaveCmdApplicationConfigurationCommand : ICommand
    {
        public Guid MessageId { get; private set; }

        public CmdApplicationConfiguration ApplicationConfiguration { get; set; }
    }
}
