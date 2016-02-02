using CommandLineApplicationLauncherModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Theory, AutoMoqData]
        public void SutCanConvertNameType(NameJsonConverter sut)
        {
            var actual = sut.CanConvert(typeof(Name));
            Assert.True(actual);
        }

        [Theory, AutoMoqData]
        public void SutCannotConvertTypeOtherThanName(NameJsonConverter sut)
        {
            Assert.False(sut.CanConvert(typeof(object)));
            Assert.False(sut.CanConvert(typeof(NameJsonConverter)));
        }

        [Theory, AutoMoqData]
        public void SutCanReadAndWriteJsonName(NameJsonConverter sut, Name aName)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(sut);
            var stringWriter = new StringWriter();
            var jsonWriter = new JsonTextWriter(stringWriter);
            serializer.Serialize(jsonWriter, aName);

            var name = serializer.Deserialize<Name>(new JsonTextReader(new StringReader(stringWriter.ToString())));
            Assert.Equal(aName, name);
        }
    }
}
