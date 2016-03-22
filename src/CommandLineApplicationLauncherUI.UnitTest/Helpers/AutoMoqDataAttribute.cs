using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;
using CommandLineApplicationLauncherModel;
using System.Collections.Generic;
using CommandLineApplicationLauncherViewModel;

namespace CommandLineApplicationLauncherUI.UnitTest
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()).Customize(new ParseCustomization()))
        {
            this.Fixture.Inject<CmdApplicationMeta>(TestCmdApplicationMeta.Application);
        }
    }

    public class ParseCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Inject<CmdApplicationConfigurationParser<string>>(new TestParser((Name)"Test", new List<IParameter>()));
        }
    }

    class TestParser : CmdApplicationConfigurationParser<string>
    {
        public Name FriendlyName { get; private set; }
        public List<IParameter> Parameters { get; private set; }

        public TestParser(Name friendlyName, List<IParameter> parameters)
        {
            this.FriendlyName = friendlyName;
            this.Parameters = parameters;
        }

        protected override Name GetFriendlyName(string data, CmdApplicationMeta applicationMeta)
        {
            return FriendlyName;
        }

        protected override IList<IParameter> GetParameters(string data, CmdApplicationMeta applicationMeta)
        {
            return Parameters;
        }
    }

}
