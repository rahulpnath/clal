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
    public class CmdApplicationTest
    {
        [Theory, AutoData]
        public void NullForCtorArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(CmdApplication).GetConstructors());
        }

        [Theory, AutoData]
        public void SutExposesName(IFixture fixture, [Frozen]Name expectedName)
        {
            var sut = fixture.Create<CmdApplication>();

            Assert.Equal(expectedName, sut.FriendlyName);
        }

        [Theory, AutoData]
        public void SutExposesApplicationName(IFixture fixture, [Frozen]Name expectedName)
        {
            var sut = fixture.Create<CmdApplication>();

            Assert.Equal(expectedName, sut.ApplicationName);
        }

        [Theory, AutoData]
        public void SutExposesApplicationName(IFixture fixture, [Frozen]IEnumerable<Name> expectedParameterNames)
        {
            var sut = fixture.Create<CmdApplication>();
            Assert.Equal(expectedParameterNames, sut.ParameterNames);

        }
    }
}
