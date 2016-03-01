using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using System;
using Xunit;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class CmdApplicationConfigurationServiceTests
    {
        [Theory, AutoMoqData]
        public void NullForCtorArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfigurationService).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfigurationService));
        }

        [Theory, AutoMoqData]
        public void ExecuteSaveCmdApplicationConfigurationCommandWithNullThrowsException(CmdApplicationConfigurationService sut)
        {
            SaveCmdApplicationConfigurationCommand NullCommand = null;
            Assert.Throws<ArgumentNullException>(() => sut.Execute(NullCommand));
        }

        [Theory, AutoMoqData]
        public void ExecuteSaveCmdApplicationConfigurationWithExistingNameRaisesRejectedEvent(
            [Frozen]Mock<ICmdApplicationConfigurationRepository> repository,
            SaveCmdApplicationConfigurationCommand command,
            TestDomainEventHandler<CmdApplicationConfigurationSaveRejected> testHandler,
            CmdApplicationConfigurationService sut)
        {
            repository
                .Setup(a => a.CheckIfConfigurationWithSameNameExists(command.ApplicationConfiguration))
                .Returns(true);
            DomainEvents.Subscribe(testHandler);
            sut.Execute(command);
            Assert.True(testHandler.EventHandlerInvoked);
        }

        [Theory, AutoMoqData]
        public void ExecuteDeleteCmdApplicationConfigurationCommandWithNullThrowsException(
            CmdApplicationConfigurationService sut)
        {
            DeleteCmdApplicationConfigurationCommand NullCommand = null;
            Assert.Throws<ArgumentNullException>(() => sut.Execute(NullCommand));
        }

        [Theory, AutoMoqData]
        public void ExecuteDeleteCmdApplicationConfigurationCommandWithNullThrowsException(
            CmdApplicationConfigurationService sut,
            TestDomainEventHandler<ConfigurationDeletedEvent> deleteHandler,
            DeleteCmdApplicationConfigurationCommand command)
        {
            DomainEvents.Subscribe(deleteHandler);
            sut.Execute(command);
            Assert.True(deleteHandler.EventHandlerInvoked);

        }
    }
}
