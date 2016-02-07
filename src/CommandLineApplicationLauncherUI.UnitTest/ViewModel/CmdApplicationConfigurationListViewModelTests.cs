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
            CmdApplicationConfigurationViewModel expected,
            CmdApplicationConfigurationListViewModel sut,
            AddCmdApplicationConfigurationEvent eventMessage)
        {
            SetUpFactoryToReturnANewInstance(expected, mockFactory);
            sut.OnAddCmdApplicationConfigurationEvent(eventMessage);
            Assert.Equal(expected, sut.SelectedConfiguration);
        }

        [Theory(Skip = "Need to inject Messenger"), AutoMoqData]
        public void SutSubscribesToAddCmdApplicationConfigurationEvent(
            Name aName,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            CmdApplicationConfigurationViewModel vm,
            [Frozen]Mock<ICmdApplicationConfigurationViewModelFactory> mockFactory,
            CmdApplicationConfigurationListViewModel sut)
        {
             SetUpFactoryToReturnANewInstance(vm, mockFactory);
            var expected = sut.ApplicationConfigurations.Count + 1;
            Messenger.Default.Send(new AddCmdApplicationConfigurationEvent());
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
