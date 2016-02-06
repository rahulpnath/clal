using CommandLineApplicationLauncherModel;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CLALSsmsParser.UnitTest
{
    public class ConnectionStringParserTests
    {
        [Theory, AutoData]
        public void SutIsCmdApplicationConfigurationParser(ConnectionStringParser sut)
        {
            Assert.IsAssignableFrom<CmdApplicationConfigurationParser<string>>(sut);
        }
    }
}
