using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public class CmdApplicationViewModel : ViewModelBase
    {
        public CmdApplicationMeta Meta { get; private set; }

        public CmdApplicationViewModel(CmdApplicationMeta meta)
        {
            if (meta == null)
                throw new ArgumentNullException(nameof(meta));

            this.Meta = meta;
        }
    }
}
