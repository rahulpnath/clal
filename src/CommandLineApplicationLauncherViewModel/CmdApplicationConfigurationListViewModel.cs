using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CommandLineApplicationLauncherViewModel
{
    public class CmdApplicationConfigurationListViewModel :
        ViewModelBase,
        IEventHandler<ConfigurationDeletedEvent>
    {
        private CmdApplicationConfigurationViewModel selectedConfiguration;
        public CmdApplicationMeta CurrentApplicationMeta { get; private set; }
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

        public IMessenger Messenger { get; private set; }
        public IChannel<DeleteCmdApplicationConfigurationCommand> DeleteChannel { get; private set; }

        public CmdApplicationConfigurationListViewModel(
            IReader<CmdApplicationMeta, IEnumerable<CmdApplicationConfiguration>> reader,
            ICmdApplicationConfigurationViewModelFactory factory,
            IChannel<DeleteCmdApplicationConfigurationCommand> deleteChannel,
            IMessenger messenger)
        {
            this.Reader = reader;
            this.Factory = factory;
            this.Messenger = messenger;
            this.DeleteChannel = deleteChannel;
            this.CurrentApplicationMeta = SsmsCmdApplication.Application;
            ApplicationConfigurations = reader
                .Query(this.CurrentApplicationMeta)
                .Select(a =>
                {
                    var vm = factory.Create(this.CurrentApplicationMeta);
                    vm.PopulateFromCmdApplicationConfiguration(a);
                    return vm;
                })
                .ToObservableCollection();
            DomainEvents.Subscribe(this);
            this.SelectedConfiguration = ApplicationConfigurations.FirstOrDefault();
            this.Messenger.Register<AddCmdApplicationConfigurationEvent>(this, this.OnAddCmdApplicationConfigurationEvent);
            this.Messenger.Register<DeleteCmdApplicationConfigurationEvent>(this, this.OnDeleteCmdApplicationConfigurationEvent);
        }

        public void OnDeleteCmdApplicationConfigurationEvent(DeleteCmdApplicationConfigurationEvent eventMessage)
        {
            var selectedConfiguration = this.SelectedConfiguration.GetCmdApplicationConfiguration();
            if (selectedConfiguration.Any())
                this.DeleteChannel.Send(
                    new DeleteCmdApplicationConfigurationCommand(Guid.NewGuid(), selectedConfiguration.First()
                    ));
        }

        public void OnAddCmdApplicationConfigurationEvent(AddCmdApplicationConfigurationEvent obj)
        {
            var newCmdApplication = Factory.Create(this.CurrentApplicationMeta);
            newCmdApplication.ToggleEdit.Execute(null);
            this.ApplicationConfigurations.Insert(0, newCmdApplication);
            this.SelectedConfiguration = newCmdApplication;
        }

        public void Handle(ConfigurationDeletedEvent eventData)
        {
            this.ApplicationConfigurations.Remove(this.SelectedConfiguration);
        }
    }
}
