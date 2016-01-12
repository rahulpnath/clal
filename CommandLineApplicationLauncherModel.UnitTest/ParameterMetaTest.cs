using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class ParameterMetaTest
    {
        [Theory,AutoData]
        public void NullForCtorArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(ParameterMeta<IParameter>));
        }

        [Theory, AutoData]
        public void ParameterTypeExposesTheCorrectType(IFixture fixture)
        {
            var sut = fixture.Create<ParameterMeta<ParameterTest>>();
            Assert.Equal(typeof(ParameterTest), sut.ParameterType);
        }

        private class ParameterTest : IParameter { }
    }
}
