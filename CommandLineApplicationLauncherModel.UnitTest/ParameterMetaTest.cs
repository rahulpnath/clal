using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using System;
using Xunit;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class ParameterMetaTest
    {
        [Theory,AutoData]
        public void NullForCtorArgumentsThrowsException(IFixture fixture)
        {
            Assert.Throws<ArgumentNullException>(() => ParameterMeta.Create<IParameter>(null));
        }

        [Theory, AutoData]
        public void ParameterTypeExposesTheCorrectType(Name name)
        {
            var sut = ParameterMeta.Create<ParameterTest>(name);
            Assert.Equal(typeof(ParameterTest), sut.ParameterType);
        }

        private class ParameterTest : IParameter { }
    }
}
