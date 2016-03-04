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
    public class CmdApplicationConfigurationViewModel :
        ViewModelBase,
        IEventHandler<ConfigurationSavedEvent>,
        IEventHandler<CmdApplicationConfigurationSaveRejected>
    {
        private string friendlyName;
        private string parseString;
        private string error;

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

        public bool IsInEditMode { get; private set; }

        public bool IsConfigurationSaved { get; private set; }

        public string ParseString
        {
            get
            {
                return this.parseString;
            }
            set
            {
                this.parseString = value;
                foreach (var parser in StringParsers)
                {
                    var v = parser.Parse(value, SsmsCmdApplication.Application);
                    if (v.Any())
                        this.PopulateFromCmdApplicationConfiguration(v.Single());
                }
            }
        }

        public List<ParameterViewModel> Properties { get; private set; }
        public System.Windows.Input.ICommand ToggleEdit { get; private set; }
        public RelayCommand Save { get; private set; }
        public System.Windows.Input.ICommand Launch { get; private set; }
        public IChannel<SaveCmdApplicationConfigurationCommand> Channel { get; private set; }
        public IEnumerable<CmdApplicationConfigurationParser<string>> StringParsers { get; private set; }
        public string Error
        {
            get
            {
                return this.error;
            }
            private set
            {
                this.error = value;
                this.RaisePropertyChanged(nameof(Error));
            }
        }

        public CmdApplicationConfigurationViewModel(
            Name applicationName,
            List<ParameterViewModel> properties,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            IEnumerable<CmdApplicationConfigurationParser<string>> stringParsers)
        {
            if (applicationName == null)
                throw new ArgumentNullException(nameof(applicationName));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if (channel == null)
                throw new ArgumentNullException(nameof(channel));


            if (stringParsers == null)
                throw new ArgumentNullException(nameof(stringParsers));

            this.ApplicationName = applicationName;
            this.Properties = properties;
            this.Channel = channel;
            this.StringParsers = stringParsers;
            this.Save = new RelayCommand(this.OnSaveExecuted, () => this.IsInEditMode);
            this.ToggleEdit = new RelayCommand(this.OnToggleEditExecuted);
            this.Launch = new RelayCommand(this.OnLaunchExecuted);
            DomainEvents.Subscribe<ConfigurationSavedEvent>(this);
            DomainEvents.Subscribe<CmdApplicationConfigurationSaveRejected>(this);
        }

        private void OnToggleEditExecuted()
        {
            this.IsInEditMode = !this.IsInEditMode;
            this.Save.RaiseCanExecuteChanged();
        }

        public Maybe<CmdApplicationConfiguration> GetCmdApplicationConfiguration()
        {
            this.Error = null;
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
            this.Error = "Saved Successfull";
            this.IsConfigurationSaved = true;
        }

        public void Handle(CmdApplicationConfigurationSaveRejected eventData)
        {
            this.Error = "Configuration friendly name already exists";
        }

        public void PopulateFromCmdApplicationConfiguration(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            this.ApplicationName = applicationConfiguration.ApplicationName;

            if (string.IsNullOrEmpty(this.FriendlyName))
                this.FriendlyName = (string)applicationConfiguration.Name;

            foreach (var property in this.Properties)
            {
                var propertyValue = applicationConfiguration.Parameters.FirstOrDefault(a => a.Name == property.GetName());
                property.WithParameter(propertyValue);
            }

            this.IsConfigurationSaved = true;
        }

        private void OnSaveExecuted()
        {
            var applicationConfiguration = this.GetCmdApplicationConfiguration();
            if (applicationConfiguration.Any())
                this.Channel.Send(
                    new SaveCmdApplicationConfigurationCommand(
                        Guid.NewGuid(),
                        applicationConfiguration.Single()));
            else
                this.Error = "Friendly Name and atleast one property should have a value";
        }

        private void OnLaunchExecuted()
        {
            var pa = string.Empty;
            foreach (var p in this.Properties)
            {
                if (p.GetParameter().Any())
                {
                    pa = pa + " " + p.GetParameter().First().GetValue();
                }
            }
            Process.Start(this.ApplicationName.ToString(), pa);
        }
    }
}
