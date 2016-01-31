using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace CommandLineApplicationLauncherJson.UnitTest.Helpers
{
    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] data)
            : base(new DataAttribute[] { new InlineDataAttribute(data), new AutoMoqDataAttribute() })
        {

        }
    }
}
