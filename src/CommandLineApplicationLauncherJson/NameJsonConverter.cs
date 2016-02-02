using CommandLineApplicationLauncherModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CommandLineApplicationLauncherJson
{
    public class NameJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Name));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (Name)(reader.Value as string);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);
            var valueAsName = value as Name;
            if (valueAsName == null)
                return;

            serializer.Serialize(writer, valueAsName.ToString());
        }
    }
}
