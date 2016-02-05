using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class NameValueParameterTest
    {
        [Theory, AutoData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(NameValueParameter));
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(NameValueParameter));
        }

        [Theory,AutoData]
        public void SutWithSameValuesAreEqual(
            Name name,
            string value)
        {
            var aParameter = new NameValueParameter(name, value);
            var anotherParameter = new NameValueParameter(name, value);
            Assert.Equal(aParameter, anotherParameter);
            Assert.Equal(aParameter.GetHashCode(), anotherParameter.GetHashCode());
        }

        [Theory]
        [InlineData("Test", "Value", "Test Value")]
        [InlineData("-Another", "'Value'", "-Another 'Value'")]
        public void GetValueReturnsExpected(string name, string value, string expected)
        {
            var sut = new NameValueParameter((Name)name, value);
            Assert.Equal(expected, sut.GetValue());
        }
    }
}
