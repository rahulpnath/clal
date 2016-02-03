using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherViewModel
{
    public interface ICmdApplicationConfigurationViewModelFactory
    {
        CmdApplicationConfigurationViewModel Create(CmdApplicationMeta meta);

        CmdApplicationConfigurationViewModel Create(CmdApplicationConfiguration applicationConfiguration);
    }

    
}
