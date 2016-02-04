using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using CommandLineApplicationLauncherViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Ploeh.AutoFixture;
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

        [Theory, AutoMoqData]
        public void SutSubscribesToAddCmdApplicationConfigurationEvent(IFixture fixture)
        {
            var sut = CreateCmdApplicationConfigurationListViewModel(fixture);
            var expected = sut.ApplicationConfigurations.Count + 1;
            Messenger.Default.Send(new AddCmdApplicationConfigurationEvent());
            Assert.Equal(expected, sut.ApplicationConfigurations.Count);
        }

        private CmdApplicationConfigurationListViewModel CreateCmdApplicationConfigurationListViewModel(IFixture fixture)
        {
            var channel = fixture.Create<IChannel<SaveCmdApplicationConfigurationCommand>>();
            fixture.Inject<ICmdApplicationConfigurationViewModelFactory>( 
                new CmdApplicationConfigurationViewModelFactory(channel));
            return fixture.Create<CmdApplicationConfigurationListViewModel>();
        }
    }
}
