using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherViewModel
{
    public class CmdApplicationConfigurationListViewModel : ViewModelBase
    {
        public IEnumerable<CmdApplicationConfigurationViewModel> ApplicationConfigurations { get; set; }
        public CmdApplicationConfigurationListViewModel(
            IReader<CmdApplicationMeta, IEnumerable<CmdApplicationConfiguration>> reader,
            ICmdApplicationConfigurationViewModelFactory factory)
        {
            ApplicationConfigurations = reader
                .Query(SsmsCmdApplication.Application)
                .Select(a => factory.Create(a, SsmsCmdApplication.Application));
        }
    }
}
