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
        [Fact]
        public void EmptyNameInCtorThrowsException()
        {
            Name nullName = null;
            Assert.Throws<ArgumentNullException>(() => new CmdApplication(nullName));
        }


        [Theory, AutoData]
        public void SutExposesName(Name name)
        {
            var sut = new CmdApplication(name);

            Assert.Equal(name, sut.FriendlyName);
        }
    }
}
