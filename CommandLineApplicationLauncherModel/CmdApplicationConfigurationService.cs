using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplicationConfigurationService : ICommandHandler<SaveCmdApplicationConfigurationCommand>
    {
        public CmdApplicationConfigurationService()
        {
            
        }

        public void Execute(SaveCmdApplicationConfigurationCommand command)
        {
        }
    }
}
