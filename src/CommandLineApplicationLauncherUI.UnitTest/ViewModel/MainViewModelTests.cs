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

        [Theory, AutoMoqData]
        public void SutHasDeleteCommandInitialized(IFixture fixture)
        {
            MainViewModel sut = CreateMainViewModel(fixture);
            Assert.NotNull(sut.DeleteCommand);
        }

        [Theory, AutoMoqData]
        public void AddCommandRaisesAddNewCmdApplicationConfigurationEvent(IFixture fixture)
        {
            var messenger = new Messenger();
            var isInvoked = false;
            messenger.Register(this, (AddCmdApplicationConfigurationEvent message) =>
            {
                isInvoked = true;
                Assert.Equal(new AddCmdApplicationConfigurationEvent(), message);
            });

            var sut = CreateMainViewModel(fixture, messenger);
            sut.AddCommand.Execute(new object());
            Assert.True(isInvoked);
        }

        [Theory, AutoMoqData]
        public void DeleteCommandRaisesAddNewCmdApplicationConfigurationEvent(IFixture fixture)
        {
            var messenger = new Messenger();
            var isInvoked = false;
            messenger.Register(this, (DeleteCmdApplicationConfigurationEvent message) =>
            {
                isInvoked = true;
                Assert.Equal(new DeleteCmdApplicationConfigurationEvent(), message);
            });

            var sut = CreateMainViewModel(fixture, messenger);
            sut.DeleteCommand.Execute(new object());
            Assert.True(isInvoked);
        }

        private MainViewModel CreateMainViewModel(IFixture fixture, IMessenger messenger = null)
        {
            var channel = fixture.Create<IChannel<SaveCmdApplicationConfigurationCommand>>();
            fixture.Register<ICmdApplicationConfigurationViewModelFactory>(() => fixture.Create<CmdApplicationConfigurationViewModelFactory>());
            fixture.Inject(messenger ?? new Messenger());
            return fixture.Build<MainViewModel>().OmitAutoProperties().Create();
        }
    }
}
