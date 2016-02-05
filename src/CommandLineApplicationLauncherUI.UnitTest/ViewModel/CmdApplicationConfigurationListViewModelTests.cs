using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using CommandLineApplicationLauncherViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Theory,AutoMoqData]
        public void OnAddNewConfigurationSelectedItemIsSetToNewItem(
            [Frozen]Mock<ICmdApplicationConfigurationViewModelFactory> mockFactory,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            CmdApplicationConfigurationListViewModel sut,
            AddCmdApplicationConfigurationEvent eventMessage)
        {
            var expected = SetUpFactoryToReturnANewInstance(channel, mockFactory);
            sut.OnAddCmdApplicationConfigurationEvent(eventMessage);
            Assert.Equal(expected, sut.SelectedConfiguration);
        }

        private CmdApplicationConfigurationViewModel SetUpFactoryToReturnANewInstance(
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            Mock<ICmdApplicationConfigurationViewModelFactory> mockFactory)
        {
            var vm = new CmdApplicationConfigurationViewModel((Name)"New", new List<ParameterViewModel>(), channel);
            mockFactory.Setup(a => a.Create(It.IsAny<CmdApplicationMeta>())).Returns(vm);
            return vm;
        }

        [Theory(Skip = "Need to inject Messenger"), AutoMoqData]
        public void SutSubscribesToAddCmdApplicationConfigurationEvent(
            Name aName,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            [Frozen]Mock<ICmdApplicationConfigurationViewModelFactory> mockFactory,
            CmdApplicationConfigurationListViewModel sut)
        {
            SetUpFactoryToReturnANewInstance(channel, mockFactory);
            var expected = sut.ApplicationConfigurations.Count + 1;
            Messenger.Default.Send(new AddCmdApplicationConfigurationEvent());
            Assert.Equal(expected, sut.ApplicationConfigurations.Count);
        }
    }
}
