﻿using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System.Linq;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class CmdApplicationConfigurationListViewModelTests
    {
        [Theory, AutoMoqData]
        public void SutIsViewModelBase(CmdApplicationConfigurationListViewModel sut)
        {
            Assert.IsAssignableFrom<ViewModelBase>(sut);
        }

        [Theory, AutoMoqData]
        public void OnAddNewConfigurationSelectedItemIsSetToNewItem(
            [Frozen]Mock<ICmdApplicationConfigurationViewModelFactory> mockFactory,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            CmdApplicationConfigurationViewModel expected,
            CmdApplicationConfigurationListViewModel sut,
            AddCmdApplicationConfigurationEvent eventMessage)
        {
            SetUpFactoryToReturnANewInstance(expected, mockFactory);
            sut.OnAddCmdApplicationConfigurationEvent(eventMessage);
            Assert.Equal(expected, sut.SelectedConfiguration);
        }

        [Theory, AutoMoqData]
        public void OnDeleteCmdApplicationConfigurationEventRemovesItem(
            CmdApplicationConfigurationListViewModel sut,
            ConfigurationDeletedEvent eventMessage)
        {
            var expected = sut.ApplicationConfigurations.Count - 1;
            SetupItemToDeleteOn(sut);
            sut.Handle(eventMessage);
            Assert.Equal(expected, sut.ApplicationConfigurations.Count);
        }

        [Theory, AutoMoqData]
        public void OnDeleteCmdApplicationConfigurationEventRaisesCommandDeleteItem(
            [Frozen]Mock<IChannel<DeleteCmdApplicationConfigurationCommand>> channel,
            CmdApplicationConfigurationListViewModel sut,
            DeleteCmdApplicationConfigurationEvent eventMessage)
        {
            SetupItemToDeleteOn(sut);

            sut.OnDeleteCmdApplicationConfigurationEvent(eventMessage);
            channel.Verify(a => a.Send(It.IsAny<DeleteCmdApplicationConfigurationCommand>()), Times.Once());
        }

        private static void SetupItemToDeleteOn(CmdApplicationConfigurationListViewModel sut)
        {
            sut.SelectedConfiguration = sut.ApplicationConfigurations.First();
            var vm = new NameOnlyParameterViewModel((Name)"testParameter");
            vm.IsSelected = true;
            sut.SelectedConfiguration.Properties.Add(vm);
        }

        [Theory, AutoMoqData]
        public void SutSubscribesToAddCmdApplicationConfigurationEvent(
            Name aName,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            CmdApplicationConfigurationViewModel vm,
            [Frozen]Mock<ICmdApplicationConfigurationViewModelFactory> mockFactory,
            Messenger messenger,
            IFixture fixture)
        {
            fixture.Inject<IMessenger>(messenger);
            var sut = fixture.Create<CmdApplicationConfigurationListViewModel>();
            SetUpFactoryToReturnANewInstance(vm, mockFactory);
            var expected = sut.ApplicationConfigurations.Count + 1;
            messenger.Send(new AddCmdApplicationConfigurationEvent());
            Assert.Equal(expected, sut.ApplicationConfigurations.Count);
        }

        private void SetUpFactoryToReturnANewInstance(
            CmdApplicationConfigurationViewModel vm,
            Mock<ICmdApplicationConfigurationViewModelFactory> mockFactory)
        {
            mockFactory.Setup(a => a.Create(It.IsAny<CmdApplicationMeta>())).Returns(vm);
        }
    }
}
