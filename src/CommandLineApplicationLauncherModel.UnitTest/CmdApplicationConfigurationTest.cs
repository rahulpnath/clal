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
    public class CmdApplicationConfigurationTest
    {
        [Theory, AutoData]
        public void NullForCtorArgumentsThrowsException(IFixture fixture, Name name)
        {
            fixture.Inject<IParameter>(new ParameterTest());
            fixture.Inject<ParameterMeta>(ParameterMeta.Create<IParameter>(name));
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfiguration).GetConstructors());
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture, Name name)
        {
            fixture.Inject<IParameter>(new ParameterTest());
            fixture.Inject<ParameterMeta>(ParameterMeta.Create<IParameter>(name));
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfiguration));
        }

        private class ParameterTest : IParameter
        {
            public Name Name
            {
                get
                {
                    return (Name)"TestParameter";
                }
            }
        }
    }
}
