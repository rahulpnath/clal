using Ploeh.AutoFixture.Xunit2;
using Xunit;
using Xunit.Sdk;

namespace CommandLineApplicationLauncherUI.UnitTest
{
    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] data)
            : base(new DataAttribute[] { new InlineDataAttribute(data), new AutoMoqDataAttribute() })
        {

        }
    }
}
