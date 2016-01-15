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
        public void NullForCtorArgumentsThrowsException(IFixture fixture, Name name)
        {
            fixture.Inject<IParameter>(new ParameterTest());
            fixture.Inject<ParameterMeta>(ParameterMeta.Create<IParameter>(name));
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(CmdApplication).GetConstructors());
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture, Name name)
        {
            fixture.Inject<IParameter>(new ParameterTest());
            fixture.Inject<ParameterMeta>(ParameterMeta.Create<IParameter>(name));
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(CmdApplication));
        }

        private class ParameterTest : IParameter { }
    }
}
