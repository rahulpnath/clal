using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace CommandLineApplicationLauncherViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; private set; }

        public ICommand DeleteCommand { get; set; }

        public CmdApplicationConfigurationListViewModel CmdApplicationConfigurationListViewModel { get; private set; }
        public IMessenger Messenger { get; private set; }

        public MainViewModel(
            ICmdApplicationConfigurationViewModelFactory factory, 
            CmdApplicationConfigurationListViewModel cmdApplicationConfigurationListViewModel,
            IMessenger messenger)
        {
            this.CmdApplicationConfigurationListViewModel = cmdApplicationConfigurationListViewModel;
            this.AddCommand = new RelayCommand(this.OnAddExecuted);
            this.DeleteCommand = new RelayCommand(this.OnDeleteExecuted);
            this.Messenger = messenger;
        }

        private void OnDeleteExecuted()
        {
            this.Messenger.Send(new DeleteCmdApplicationConfigurationEvent());
        }

        private void OnAddExecuted()
        {
            this.Messenger.Send(new AddCmdApplicationConfigurationEvent());
        }
    }
}