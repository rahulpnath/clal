using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherViewModel;
using Ploeh.AutoFixture;
using Xunit;
using GalaSoft.MvvmLight.Messaging;

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

        [Theory, AutoMoqData]
        public void AddCommandRaisesAddNewCmdApplicationConfigurationEvent(IFixture fixture)
        {
            Messenger.Reset();
            Messenger.Default.Register(this, (AddCmdApplicationConfigurationEvent message) =>
            {
                Assert.Equal(new AddCmdApplicationConfigurationEvent(), message);
            });

            var sut = CreateMainViewModel(fixture);
            sut.AddCommand.Execute(null);
        }

        private MainViewModel CreateMainViewModel(IFixture fixture)
        {
            var channel = fixture.Create<IChannel<SaveCmdApplicationConfigurationCommand>>();
            return new MainViewModel(new CmdApplicationConfigurationViewModelFactory(channel));
        }
    }
}
