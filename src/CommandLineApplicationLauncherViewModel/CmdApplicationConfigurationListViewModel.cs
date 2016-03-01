using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
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
            ApplicationConfigurations = reader
                .Query(SsmsCmdApplication.Application)
                .Select(a =>
                {
                    var vm = factory.Create(SsmsCmdApplication.Application);
                    vm.PopulateFromCmdApplicationConfiguration(a);
                    return vm;
                })
                .ToObservableCollection();
            this.SelectedConfiguration = ApplicationConfigurations.FirstOrDefault();
            this.Messenger.Register<AddCmdApplicationConfigurationEvent>(this, this.OnAddCmdApplicationConfigurationEvent);
            this.Messenger.Register<DeleteCmdApplicationConfigurationEvent>(this, this.OnDeleteCmdApplicationConfigurationEvent);
        }

        public void OnDeleteCmdApplicationConfigurationEvent(DeleteCmdApplicationConfigurationEvent eventMessage)
        {
            this.DeleteChannel.Send(
                new DeleteCmdApplicationConfigurationCommand(Guid.NewGuid(), 
                this.SelectedConfiguration.GetCmdApplicationConfiguration().First()));
            this.ApplicationConfigurations.Remove(this.SelectedConfiguration);
        }

        public void OnAddCmdApplicationConfigurationEvent(AddCmdApplicationConfigurationEvent obj)
        {
            var newCmdApplicationEvent = Factory.Create(SsmsCmdApplication.Application);
            this.ApplicationConfigurations.Insert(0, newCmdApplicationEvent);
            this.SelectedConfiguration = newCmdApplicationEvent;
        }
    }
}
