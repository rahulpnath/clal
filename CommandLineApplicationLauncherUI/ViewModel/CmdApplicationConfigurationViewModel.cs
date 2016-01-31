using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public class CmdApplicationConfigurationViewModel : ViewModelBase, IEventHandler<ConfigurationSavedEvent>
    {
        public Name ApplicationName { get; private set; }
        public string FriendlyName { get; set; }
        public List<ParameterViewModel> Properties { get; private set; }
        public System.Windows.Input.ICommand Save { get; private set; }
        public IChannel<SaveCmdApplicationConfigurationCommand> Channel { get; private set; }

        public CmdApplicationConfigurationViewModel(
            Name applicationName,
            List<ParameterViewModel> properties,
            IChannel<SaveCmdApplicationConfigurationCommand> channel)
        {
            if (applicationName == null)
                throw new ArgumentNullException(nameof(applicationName));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if (channel == null)
                throw new ArgumentNullException(nameof(channel));

            this.ApplicationName = applicationName;
            this.Properties = properties;
            this.Channel = channel;
            this.Save = new RelayCommand(this.OnSaveExecuted);
            DomainEvents.Subscribe(this);
        }

        private void OnSaveExecuted()
        {
            var cmd = new CmdApplicationConfiguration((Name)FriendlyName, (Name)ApplicationName, new System.Collections.ObjectModel.ReadOnlyCollection<IParameter>(new List<IParameter>()));
            this.Channel.Send(new SaveCmdApplicationConfigurationCommand() { ApplicationConfiguration = cmd });
        }

        public void Handle(ConfigurationSavedEvent command)
        {
        }
    }
}
