﻿using CommandLineApplicationLauncherModel;
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
        private bool isInEditMode;
        private CmdApplicationConfiguration lastSavedConfiguration;

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

        public bool IsInEditMode
        {
            get
            {
                return isInEditMode;
            }
            private set
            {
                this.isInEditMode = value;
                this.RaisePropertyChanged(nameof(IsInEditMode));
            }
        }

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
                    var v = parser.Parse(value, this.ApplicationMeta);
                    if (v.Any())
                        this.PopulateFromCmdApplicationConfiguration(v.Single());
                }
            }
        }

        public CmdApplicationMeta ApplicationMeta { get; private set; }
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
            CmdApplicationMeta applicationMeta,
            List<ParameterViewModel> properties,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            IEnumerable<CmdApplicationConfigurationParser<string>> stringParsers)
        {
            if (applicationMeta == null)
                throw new ArgumentNullException(nameof(applicationMeta));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if (channel == null)
                throw new ArgumentNullException(nameof(channel));


            if (stringParsers == null)
                throw new ArgumentNullException(nameof(stringParsers));

            this.ApplicationMeta = applicationMeta;
            this.ApplicationName = applicationMeta.ApplicationName;
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
            this.PopulateFromCmdApplicationConfiguration(this.lastSavedConfiguration);

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
            this.IsInEditMode = false;
            this.lastSavedConfiguration = this.GetCmdApplicationConfiguration().FirstOrDefault();
            this.PopulateFromCmdApplicationConfiguration(this.lastSavedConfiguration);
        }

        public void Handle(CmdApplicationConfigurationSaveRejected eventData)
        {
            this.Error = "Configuration friendly name already exists";
        }

        public void PopulateFromCmdApplicationConfiguration(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                return;

            lastSavedConfiguration = applicationConfiguration;

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
