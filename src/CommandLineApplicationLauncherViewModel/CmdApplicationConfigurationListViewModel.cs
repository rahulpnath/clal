using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CommandLineApplicationLauncherViewModel
{
    public class CmdApplicationConfigurationListViewModel : ViewModelBase
    {
        private CmdApplicationConfigurationViewModel selectedConfiguration;
        public ObservableCollection<CmdApplicationConfigurationViewModel> ApplicationConfigurations { get; set; }
        public IReader<CmdApplicationMeta, IEnumerable<CmdApplicationConfiguration>> Reader { get; private set; }
        public ICmdApplicationConfigurationViewModelFactory Factory { get; private set; }
        public CmdApplicationConfigurationViewModel SelectedConfiguration
        {
            get
            {
                return this.selectedConfiguration;
            }
            set
            {
                this.selectedConfiguration = value;
                this.RaisePropertyChanged(nameof(SelectedConfiguration));
            }
        }

        public CmdApplicationConfigurationListViewModel(
            IReader<CmdApplicationMeta, IEnumerable<CmdApplicationConfiguration>> reader,
            ICmdApplicationConfigurationViewModelFactory factory)
        {
            this.Reader = reader;
            this.Factory = factory;
            ApplicationConfigurations = reader
                .Query(SsmsCmdApplication.Application)
                .Select(a => factory.Create(a, SsmsCmdApplication.Application)).ToObservableCollection();
            this.SelectedConfiguration = ApplicationConfigurations.FirstOrDefault();
            MessengerInstance.Register<AddCmdApplicationConfigurationEvent>(this, this.OnAddCmdApplicationConfigurationEvent);
        }

        public void OnAddCmdApplicationConfigurationEvent(AddCmdApplicationConfigurationEvent obj)
        {
            var newCmdApplicationEvent = Factory.Create(SsmsCmdApplication.Application);
            this.ApplicationConfigurations.Insert(0, newCmdApplicationEvent);
            this.SelectedConfiguration = newCmdApplicationEvent;
        }
    }
}
