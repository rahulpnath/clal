using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherJson.UnitTest
{
    public class NameJsonConverterTests
    {
        [Theory, AutoMoqData]
        public void SutIsJsonConverter(NameJsonConverter sut)
        {
            Assert.IsAssignableFrom<JsonConverter>(sut);
        }
    }
}
