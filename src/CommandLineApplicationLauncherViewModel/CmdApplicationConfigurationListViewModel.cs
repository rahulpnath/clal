using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherViewModel
{
    public class CmdApplicationConfigurationListViewModel : ViewModelBase
    {

        public ObservableCollection<CmdApplicationConfigurationViewModel> ApplicationConfigurations { get; set; }
        public IReader<CmdApplicationMeta, IEnumerable<CmdApplicationConfiguration>> Reader { get; private set; }
        public ICmdApplicationConfigurationViewModelFactory Factory { get; private set; }

        public CmdApplicationConfigurationListViewModel(
            IReader<CmdApplicationMeta, IEnumerable<CmdApplicationConfiguration>> reader,
            ICmdApplicationConfigurationViewModelFactory factory)
        {
            this.Reader = reader;
            this.Factory = factory;
            ApplicationConfigurations = reader
                .Query(SsmsCmdApplication.Application)
                .Select(a => factory.Create(a, SsmsCmdApplication.Application)).ToObservableCollection(); ;
            MessengerInstance.Register<AddCmdApplicationConfigurationEvent>(this, this.OnAddCmdApplicationConfigurationEvent);
        }

        private void OnAddCmdApplicationConfigurationEvent(AddCmdApplicationConfigurationEvent obj)
        {
            var newCmdApplicationEvent = Factory.Create(SsmsCmdApplication.Application);
            this.ApplicationConfigurations.Insert(0,newCmdApplicationEvent);
        }
    }
}
