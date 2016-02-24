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
        public void SutHasAddCommandInitialized(IFixture fixture)
        {
            MainViewModel sut = CreateMainViewModel(fixture);
            Assert.NotNull(sut.AddCommand);
        }

        [Theory(Skip ="Failing when run together. need to investigate"), AutoMoqData]
        public void AddCommandRaisesAddNewCmdApplicationConfigurationEvent(IFixture fixture)
        {
            // TODO: Better way to test these. there is still likelyhood of these failing
            Messenger.Reset();
            Messenger.Default.Register(this, (AddCmdApplicationConfigurationEvent message) =>
            {
                Assert.Equal(new AddCmdApplicationConfigurationEvent(), message);
            });

            var sut = CreateMainViewModel(fixture);
            sut.AddCommand.Execute(new object());
        }

        private MainViewModel CreateMainViewModel(IFixture fixture)
        {
            var channel = fixture.Create<IChannel<SaveCmdApplicationConfigurationCommand>>();
            fixture.Register<ICmdApplicationConfigurationViewModelFactory>(() => fixture.Create<CmdApplicationConfigurationViewModelFactory>());
            return fixture.Create<MainViewModel>();
        }
    }
}
