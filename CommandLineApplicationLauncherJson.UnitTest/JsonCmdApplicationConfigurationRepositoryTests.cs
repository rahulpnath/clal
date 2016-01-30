using CommandLineApplicationLauncherJson.UnitTest.Helpers;
using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherPersistenceModel;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
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
        public void CheckIfConfigurationWithSameNameExistsWithNullValueThrowsException(
            JsonCmdApplicationConfigurationRepository sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.CheckIfConfigurationWithSameNameExists(null));
        }

        [Theory]
        [InlineAutoMoqData("appname", "friendlyname", "appname-friendlyname", true, true)]
        [InlineAutoMoqData("appname", "friendly name", "appname-friendly_name", true, true)]
        [InlineAutoMoqData("appname", "friendlyname", "appname-friendlyname", false, false)]
        [InlineAutoMoqData("appname", "friendlyname", "anotherappname-friendlyname", true, false)]
        public void CheckIfConfigurationWithSameNameExistsReturnsExpectedValue(
            string applicationName,
            string friendlyName,
            string fileName,
            bool fileExists,
            bool expected,
            IFixture fixture,
            [Frozen]Mock<IStoreReader<string>> storeReader,
            JsonCmdApplicationConfigurationRepository sut)
        {
            var applicationConfiguration = GetApplicationConfiguration(applicationName, friendlyName, fixture);
            storeReader.Setup(a => a.CheckIfFileExists(fileName)).Returns(fileExists);
            var actual = sut.CheckIfConfigurationWithSameNameExists(applicationConfiguration);
            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void GetConfigurationFileNameWithNullValueThrowsException(JsonCmdApplicationConfigurationRepository sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetConfigurationFileName(null));
        }

        [Theory]
        [InlineAutoMoqData("appName", "friendly Name", "appname-friendly_name")]
        [InlineAutoMoqData("appNameAnother", "friendly Name", "appnameanother-friendly_name")]
        public void GetConfigurationFileNameReturnsExpectedValue(
            string applicationName,
            string friendlyName,
            string expected,
            JsonCmdApplicationConfigurationRepository sut,
            IFixture fixture)
        {
            var applicationConfiguration = GetApplicationConfiguration(applicationName, friendlyName, fixture);

            var actual = sut.GetConfigurationFileName(applicationConfiguration);

            Assert.Equal(expected, actual);
        }

        private static CmdApplicationConfiguration GetApplicationConfiguration(string applicationName, string friendlyName, IFixture fixture)
        {
            // TODO: This can be generalized 
            return fixture.Build<CmdApplicationConfiguration>()
                .FromSeed(a => new CmdApplicationConfiguration(
                    (Name)friendlyName,
                    (Name)applicationName,
                    new System.Collections.ObjectModel.ReadOnlyCollection<IParameter>(new List<IParameter>())))
                    .Create();
        }
    }
}
