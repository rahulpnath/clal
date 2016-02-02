using CommandLineApplicationLauncherFilePersistence;
using CommandLineApplicationLauncherModel;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CLALFilePersistence.Test
{
    public class FileNameStoreTests
    {
        [Theory, AutoData]
        public void GetConfigurationFileNameWithNullValueThrowsException(FileNameStore sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetConfigurationFileName(null));
        }

        [Theory]
        [InlineAutoMoqData("appName", "friendly Name", "appname-friendly_name.json")]
        [InlineAutoMoqData("appNameAnother", "friendly Name", "appnameanother-friendly_name.json")]
        public void GetConfigurationFileNameReturnsExpectedValue(
            string applicationName,
            string friendlyName,
            string expected,
            FileNameStore sut,
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
