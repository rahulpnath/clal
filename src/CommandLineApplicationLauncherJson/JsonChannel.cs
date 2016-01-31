using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherPersistenceModel;
using Newtonsoft.Json;
using System.IO;

namespace CommandLineApplicationLauncherJson
{
    public class JsonChannel<T> : IChannel<T> where T : IMessage
    {
        private readonly JsonSerializer serializer;
        private readonly IStoreWriter<T> store;

        public JsonChannel(IStoreWriter<T> store)
        {
            this.store = store;
            this.serializer = new JsonSerializer();
        }

        public void Send(T message)
        {
            using (var stream = this.store.OpenStreamFor(message))
            using (var writer = new StreamWriter(stream))
                this.serializer.Serialize(writer, message);
        }
    }
}
