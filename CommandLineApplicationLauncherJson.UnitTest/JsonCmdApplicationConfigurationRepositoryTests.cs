using CommandLineApplicationLauncherJson.UnitTest.Helpers;
using CommandLineApplicationLauncherModel;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Theory, AutoMoqData]
        public void GetConfigurationFileNameWithNullValueThrowsException(JsonCmdApplicationConfigurationRepository sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetConfigurationFileName(null));
        }

        [Theory]
        [InlineAutoMoqData("appName","friendly Name", "appname-friendly_name")]
        [InlineAutoMoqData("appNameAnother", "friendly Name", "appnameanother-friendly_name")]
        public void GetConfigurationFileNameReturnsExpectedValue(
            string applicationName,
            string friendlyName,
            string expected,
            JsonCmdApplicationConfigurationRepository sut,
            IFixture fixture)
        {
            // TODO: This can be generalized 
            var applicationConfiguration = fixture.Build<CmdApplicationConfiguration>()
                .FromSeed(a => new CmdApplicationConfiguration(
                    (Name)friendlyName,
                    (Name)applicationName,
                    new System.Collections.ObjectModel.ReadOnlyCollection<IParameter>(new List<IParameter>())))
                    .Create();

            var actual = sut.GetConfigurationFileName(applicationConfiguration);

            Assert.Equal(expected, actual);
        }
    }
}
