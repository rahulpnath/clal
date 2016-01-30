using CommandLineApplicationLauncherModel;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;

using Xunit;

namespace CommandLineApplicationLauncherJson.UnitTest
{
    public class JsonCmdApplicationConfigurationRepositoryTests
    {
        [Theory, AutoMoqData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(JsonCmdApplicationConfigurationRepository).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(JsonCmdApplicationConfigurationRepository).GetConstructors());
        }
    }
}
