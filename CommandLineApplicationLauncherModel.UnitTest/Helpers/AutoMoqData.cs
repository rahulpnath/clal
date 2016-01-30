using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class AutoMoqData : AutoDataAttribute
    {
        public AutoMoqData(): base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
