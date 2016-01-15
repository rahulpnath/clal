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

        public string FriendlyName { get; set; }

        public CmdApplicationConfigurationViewModel(CmdApplicationMeta meta)
        {
            if (meta == null)
                throw new ArgumentNullException(nameof(meta));

            this.Meta = meta;
        }
    }
}
