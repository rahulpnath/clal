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
        public void MainViewModelSetToSsmsApplicationByDefault(IChannel<SaveCmdApplicationConfigurationCommand> channel)
        {
            MainViewModel sut = new MainViewModel(new CmdApplicationConfigurationViewModelFactory(channel));
            Assert.Equal(
                SsmsCmdApplication.Application.ApplicationName,
                sut.CmdApplicationConfigurationViewModel.ApplicationName);
        }

        [Theory, AutoMoqData]
        public void SutHasAddCommandInitialized(IChannel<SaveCmdApplicationConfigurationCommand> channel)
        {
            MainViewModel sut = new MainViewModel(new CmdApplicationConfigurationViewModelFactory(channel));
            Assert.NotNull(sut.AddCommand);
        }
    }
}
