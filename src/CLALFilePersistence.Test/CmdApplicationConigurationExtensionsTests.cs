using CommandLineApplicationLauncherFilePersistence;
using CommandLineApplicationLauncherModel;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using Xunit;

namespace CLALFilePersistence.Test
{
    public class CmdApplicationConigurationExtensionsTests
    {
        [Fact]
        public void GetConfigurationFileNameWithNullValueThrowsException()
        {
            CmdApplicationConfiguration sut = null;
            Assert.Throws<ArgumentNullException>(() => sut.GetFileName(null));
        }

        [Theory, AutoMoqData]
        public void GetConfigurationFileNameWithNullValueThrowsException(CmdApplicationConfiguration sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetFileName(null));
        }

        [Theory]
        [InlineAutoMoqData("appName", "friendly Name", "json", "appname-friendly_name.json")]
        [InlineAutoMoqData("appNameAnother", "friendly Name", "json", "appnameanother-friendly_name.json")]
        public void GetConfigurationFileNameReturnsExpectedValue(
            string applicationName,
            string friendlyName,
            string extension,
            string expected,
            IFixture fixture)
        {

            var applicationConfiguration = GetApplicationConfiguration(applicationName, friendlyName, fixture);

            var actual = applicationConfiguration.GetFileName(extension);

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
