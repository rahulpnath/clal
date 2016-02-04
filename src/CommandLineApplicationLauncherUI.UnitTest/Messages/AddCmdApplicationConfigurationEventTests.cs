using CommandLineApplicationLauncherViewModel;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.Messages
{
    public class AddCmdApplicationConfigurationEventTests
    {
        [Theory,AutoMoqData]
        public void TwoSutAreEqual(IFixture fixture)
        {
            var assertion = new CompositeIdiomaticAssertion(
                new EqualsNullAssertion(fixture),
                new EqualsSelfAssertion(fixture),
                new EqualsNewObjectAssertion(fixture),
                new EqualsSuccessiveAssertion(fixture));
            assertion.Verify(typeof(AddCmdApplicationConfigurationEvent));
        }
    }
}
