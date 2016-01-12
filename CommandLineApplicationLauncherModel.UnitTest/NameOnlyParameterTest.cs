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
    public class NameOnlyParameterTest
    {
        [Theory, AutoData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(NameOnlyParameter));
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(NameOnlyParameter));
        }

        [Theory, AutoData]
        public void SutWithSameValuesAreEqual(Name name)
        {
            var aParameter = new NameOnlyParameter(name);
            var anotherParameter = new NameOnlyParameter(name);
            Assert.Equal(aParameter, anotherParameter);
            Assert.Equal(aParameter.GetHashCode(), anotherParameter.GetHashCode());
        }
    }
}
