using CommandLineApplicationLauncherFilePersistence;
using CommandLineApplicationLauncherModel;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CLALFilePersistence.Test
{
    public class CmdApplicationMetaTests
    {
        [Fact]
        public void GetDirectoryOnNullThrowsException()
        {
            CmdApplicationMeta sut = null;
            Assert.Throws<ArgumentNullException>(() => sut.GetConfigurationDirectoryInfo(string.Empty));
        }

        [Theory]
        [InlineAutoMoqData("appName", "friendly Name", "appName")]
        [InlineAutoMoqData("appNameAnother", "friendly Name", "appNameAnother")]
        public void GetConfigurationDirectoryNameExpectedValue(
            string applicationName,
            string friendlyName,
            string expected,
            IFixture fixture)
        {
            var applicationMeta = GetApplicationConfigurationMeta(applicationName, friendlyName, fixture);
            var actual = applicationMeta.GetConfigurationDirectoryName();
            Assert.Equal(expected, actual);
        }

        private CmdApplicationMeta GetApplicationConfigurationMeta(string applicationName, string friendlyName, IFixture fixture)
        {
            // TODO: This can be generalized 
            fixture.Inject<Type>(typeof(IParameter));
            return fixture.Build<CmdApplicationMeta>()
                .FromSeed(a => new CmdApplicationMeta(
                    (Name)friendlyName,
                    (Name)applicationName,
                    new List<ParameterMeta>()))
                    .Create();
        }
    }
}
