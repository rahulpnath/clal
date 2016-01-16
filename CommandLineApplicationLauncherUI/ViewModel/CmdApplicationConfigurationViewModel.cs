using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public class CmdApplicationConfigurationViewModel : ViewModelBase
    {
        public CmdApplicationMeta Meta { get; private set; }

        public Name ApplicationName { get; private set; }

        public string FriendlyName { get; set; }

        public List<ParameterViewModel> Properties { get; private set; }

        public CmdApplicationConfigurationViewModel(Name applicationName, List<ParameterViewModel> properties)
        {
            if (applicationName == null)
                throw new ArgumentNullException(nameof(applicationName));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            
        }
    }
}
