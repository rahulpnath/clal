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

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData("abcde")]
        [InlineAutoMoqData("server=(local);user id=ab;pass= a!Pass113;initial catalog=AdventureWorks")]
        public void SutWithInvalidConnectionStringFormatReturnsEmpty(
            string invalidConnectionString,
            ConnectionStringParser sut)
        {
            var expected = Maybe.Empty<CmdApplicationConfiguration>();
            var actual = sut.Parse(invalidConnectionString, SsmsCmdApplication.Application);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineAutoMoqData("server=(local);user id=ab;password= a!Pass113;initial catalog=AdventureWorks", 4)]
        [InlineAutoMoqData("server=(local);Integrated Security = SSPI;;initial catalog=AdventureWorks", 3)]
        public void SutWithValidConnectionStringFormatReturnsConfiguration(
            string invalidConnectionString,
            int expected,
            ConnectionStringParser sut)
        {
            var actual = sut.Parse(invalidConnectionString, SsmsCmdApplication.Application);
            Assert.Equal(expected, actual.First().Parameters.Count);
        }
    }
}
