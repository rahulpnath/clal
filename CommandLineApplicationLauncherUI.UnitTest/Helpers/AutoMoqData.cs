using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace CommandLineApplicationLauncherUI.UnitTest
{
    public class AutoMoqData : AutoDataAttribute
    {
        public AutoMoqData(): base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
