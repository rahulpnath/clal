using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using System;

namespace CommandLineApplicationLauncherViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; private set; }

        public ICommand DeleteCommand { get; set; }

        public CmdApplicationConfigurationListViewModel CmdApplicationConfigurationListViewModel { get; private set; }

        public MainViewModel(
            ICmdApplicationConfigurationViewModelFactory factory, 
            CmdApplicationConfigurationListViewModel cmdApplicationConfigurationListViewModel)
        {
            this.CmdApplicationConfigurationListViewModel = cmdApplicationConfigurationListViewModel;
            this.AddCommand = new RelayCommand(this.OnAddExecuted);
            this.DeleteCommand = new RelayCommand(this.OnDeleteExecuted);
        }

        private void OnDeleteExecuted()
        {
            throw new NotImplementedException();
        }

        private void OnAddExecuted()
        {
            Messenger.Default.Send(new AddCmdApplicationConfigurationEvent());
        }
    }
}