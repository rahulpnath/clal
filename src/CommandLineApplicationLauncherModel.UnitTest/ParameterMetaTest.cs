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

        [Theory, AutoData]
        public void SutWithDefaultDisplayNameReturnsEmptyName(Name aName)
        {
            var sut = ParameterMeta.Create<ParameterTest>(aName);
            Assert.Equal(Name.EmptyName, sut.DisplayName);
        }


        [Theory, AutoData]
        public void CtorWithTypeNotIParameterThrowsException(Name aName, Name aDisplayName)
        {
            Assert.Throws<ArgumentException>(() => new ParameterMeta(aName, typeof(object), aDisplayName));
        }

        [Theory, AutoData]
        public void CtorWithTypeIParameterCreatesSut(Name aName, Name aDisplayName)
        {
            var sut = new ParameterMeta(aName, typeof(ParameterTest), aDisplayName);
            Assert.NotNull(sut);
        }

        [Theory, AutoData]
        public void SutWithDisplayNameReturnsName(Name aName, Name aDisplayName)
        {
            var sut = ParameterMeta.Create<ParameterTest>(aName, aDisplayName);
            Assert.Equal(aDisplayName, sut.DisplayName);
        }

        private class ParameterTest : IParameter { }
    }
}
