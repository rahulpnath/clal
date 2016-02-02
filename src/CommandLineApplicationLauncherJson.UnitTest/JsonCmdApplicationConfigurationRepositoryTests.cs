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
        [InlineAutoMoqData("appname", "friendlyname", true, true)]
        [InlineAutoMoqData("appname", "friendlyname", false, false)]
        public void CheckIfConfigurationWithSameNameExistsReturnsExpectedValue(
            string applicationName,
            string friendlyName,
            bool fileExists,
            bool expected,
            IFixture fixture,
            [Frozen]Mock<IStoreReader<CmdApplicationConfiguration>> storeReader,
            JsonCmdApplicationConfigurationRepository sut)
        {
            var applicationConfiguration = GetApplicationConfiguration(applicationName, friendlyName, fixture);
            storeReader.Setup(a => a.CheckIfFileExists(applicationConfiguration)).Returns(fileExists);
            var actual = sut.CheckIfConfigurationWithSameNameExists(applicationConfiguration);
            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void CreateNewConfigurationWithNullValueThrowsException(JsonCmdApplicationConfigurationRepository sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.CreateNewConfiguration(null));
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
