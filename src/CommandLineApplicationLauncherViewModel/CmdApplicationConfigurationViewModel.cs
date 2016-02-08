using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public System.Windows.Input.ICommand Launch { get; private set; }
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
            this.Launch = new RelayCommand(this.OnLaunchExecuted);
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

        public void Handle(ConfigurationSavedEvent command)
        {
        }

        public void PopulateFromCmdApplicationConfiguration(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));
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

        private void OnLaunchExecuted()
        {
            var pa = string.Empty;
            foreach(var p in this.Properties)
            {
                if(p.GetParameter().Any())
                {
                    pa = pa+ " "+ p.GetParameter().First().GetValue();
                }
            }
            Process.Start(this.ApplicationName.ToString(), pa);
        }
    }
}
