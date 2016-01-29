using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public interface ICmdApplicationConfigurationViewModelFactory
    {
        CmdApplicationConfigurationViewModel Create(CmdApplicationMeta meta);
    }

    
}
