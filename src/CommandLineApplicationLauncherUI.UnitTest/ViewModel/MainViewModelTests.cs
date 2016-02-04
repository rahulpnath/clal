using System;
using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using CommandLineApplicationLauncherViewModel;
using Ploeh.AutoFixture;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class MainViewModelTests
    {
        [Theory, AutoMoqData]
        public void MainViewModelSetToSsmsApplicationByDefault(IFixture fixture)
        {
            MainViewModel sut =  CreateMainViewModel(fixture);
            Assert.Equal(
                SsmsCmdApplication.Application.ApplicationName,
                sut.CmdApplicationConfigurationViewModel.ApplicationName);
        }

        [Theory, AutoMoqData]
        public void SutHasAddCommandInitialized(IFixture fixture)
        {
            MainViewModel sut = CreateMainViewModel(fixture);
            Assert.NotNull(sut.AddCommand);
        }

        private MainViewModel CreateMainViewModel(IFixture fixture)
        {
            var channel = fixture.Create<IChannel<SaveCmdApplicationConfigurationCommand>>();
            return new MainViewModel(new CmdApplicationConfigurationViewModelFactory(channel));
        }
    }
}
