using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CommandLineApplicationLauncherViewModel
{
    public class CmdApplicationConfigurationViewModel : ViewModelBase, IEventHandler<ConfigurationSavedEvent>
    {
        private string friendlyName;

        public Name ApplicationName { get; private set; }
        public string FriendlyName
        {
            get
            {
                return this.friendlyName;
            }
            set
            {
                this.friendlyName = value;
                RaisePropertyChanged(nameof(FriendlyName));
            }
        }
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

        public Maybe<CmdApplicationConfiguration> GetCmdApplicationConfiguration()
        {
            if (string.IsNullOrEmpty(this.FriendlyName))
                return Maybe.Empty<CmdApplicationConfiguration>();

            var parameters = new List<IParameter>();
            foreach (var parameterVm in this.Properties)
                parameters.AddRange(parameterVm.GetParameter());

            if (!parameters.Any())
                return Maybe.Empty<CmdApplicationConfiguration>();

            var applicationConfiguration = new CmdApplicationConfiguration(
                (Name)FriendlyName,
                ApplicationName,
                new ReadOnlyCollection<IParameter>(parameters));

            return Maybe.ToMaybe(applicationConfiguration);
        }

        private void OnSaveExecuted()
        {
            var applicationConfiguration = this.GetCmdApplicationConfiguration();
            if (applicationConfiguration.Any())
                this.Channel.Send(
                    new SaveCmdApplicationConfigurationCommand(
                        Guid.NewGuid(),
                        applicationConfiguration.Single()));
        }

        public void Handle(ConfigurationSavedEvent command)
        {
        }
    }
}
