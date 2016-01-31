using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace CommandLineApplicationLauncherJson.UnitTest
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(): base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
