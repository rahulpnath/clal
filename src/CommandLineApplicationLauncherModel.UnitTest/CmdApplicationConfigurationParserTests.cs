using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class CmdApplicationConfigurationParserTests
    {
        [Fact]
        public void SutIsAbstract()
        {
            Assert.True(typeof(CmdApplicationConfigurationParser<>).IsAbstract);
        }

        [Fact]
        public void SutReturnsEmptyWhenParametersIsNullOrEmpty()
        {
            var expected = Maybe.Empty<CmdApplicationConfiguration>();
            var name = (Name)"AnyName";
            var parser = new TestParser(name, null);
            var actual = parser.Parse(new object(), GetApplicationConfigurationMeta());
            Assert.Equal(expected, actual);
        }

        private CmdApplicationMeta GetApplicationConfigurationMeta()
        {
            return new CmdApplicationMeta(
                    (Name)"FriendlyName",
                    (Name)"ApplicationName",
                    new List<ParameterMeta>()
                    {
                        ParameterMeta.Create<NameOnlyParameter>((Name)"aParameter"),
                        ParameterMeta.Create<NameValueParameter>((Name)"anotherParameter")
                    });
                    
        }
    }

    class TestParser : CmdApplicationConfigurationParser<object>
    {
        public Name FriendlyName { get; private set; }
        public List<IParameter> Parameters { get; private set; }

        public TestParser(Name friendlyName, List<IParameter> parameters)
        {
            this.FriendlyName = friendlyName;
            this.Parameters = parameters;
        }

        protected override Name GetFriendlyName(object data, CmdApplicationMeta applicationMeta)
        {
            return FriendlyName;
        }

        protected override IList<IParameter> GetParameters(object data, CmdApplicationMeta applicationMeta)
        {
            return Parameters;
        }
    }
}
