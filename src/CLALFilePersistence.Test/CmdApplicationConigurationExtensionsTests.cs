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
        public void GetFileNameOnNullThrowsException()
        {
            CmdApplicationConfiguration sut = null;
            Assert.Throws<ArgumentNullException>(() => sut.GetFileName(null));
        }

        [Fact]
        public void GetDirectoryOnNullThrowsException()
        {
            CmdApplicationConfiguration sut = null;
            Assert.Throws<ArgumentNullException>(() => sut.GetDirectoryName());
        }

        [Theory]
        [InlineAutoMoqData("appName", "friendly Name", "appName")]
        [InlineAutoMoqData("appNameAnother", "friendly Name", "appNameAnother")]
        public void GetFileNameReturnsExpectedValue(
            string applicationName,
            string friendlyName,
            string expected,
            IFixture fixture)
        {
            var applicationConfiguration = GetApplicationConfiguration(applicationName, friendlyName, fixture);
            var actual = applicationConfiguration.GetDirectoryName();
            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void GetFileNameWithNullValueThrowsException(CmdApplicationConfiguration sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetFileName(null));
        }

        [Theory]
        [InlineAutoMoqData("appName", "friendly Name", "json", "appname-friendly_name.json")]
        [InlineAutoMoqData("appNameAnother", "friendly Name", "json", "appnameanother-friendly_name.json")]
        public void GetFileNameReturnsExpectedValue(
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
