using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CommandLineApplicationLauncherViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public System.Windows.Input.ICommand AddCommand { get; private set; }

        public CmdApplicationConfigurationListViewModel CmdApplicationConfigurationListViewModel { get; private set; }

        public MainViewModel(
            ICmdApplicationConfigurationViewModelFactory factory, 
            CmdApplicationConfigurationListViewModel cmdApplicationConfigurationListViewModel)
        {
            this.CmdApplicationConfigurationListViewModel = cmdApplicationConfigurationListViewModel;
            this.AddCommand = new RelayCommand(this.OnAddExecuted);
        }

        private void OnAddExecuted()
        {
            Messenger.Default.Send(new AddCmdApplicationConfigurationEvent());
        }
    }
}