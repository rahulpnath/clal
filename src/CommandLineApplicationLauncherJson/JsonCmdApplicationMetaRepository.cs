using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherPersistenceModel;
using CommandLineApplicationLauncherViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherJson
{
    public class JsonCmdApplicationMetaRepository : IReader<CmdApplicationMeta, IEnumerable<CmdApplicationConfiguration>>
    {
        private readonly JsonSerializer serializer;

        public IStoreReader<CmdApplicationMeta> StoreReader { get; private set; }

        public JsonCmdApplicationMetaRepository(IStoreReader<CmdApplicationMeta> storeReader)
        {
            if (storeReader == null)
                throw new ArgumentNullException(nameof(storeReader));

            this.StoreReader = storeReader;
            this.serializer = new JsonSerializer();
            this.serializer.Converters.Add(new NameJsonConverter());
            this.serializer.TypeNameHandling = TypeNameHandling.Auto;
        }

        public IEnumerable<CmdApplicationConfiguration> Query(CmdApplicationMeta criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));
            
            var streams = this.StoreReader.OpenStreamsFor(criteria);
            foreach (var stream in streams)
            {
                using (var jsonReader = new JsonTextReader(new StreamReader(stream)))
                {
                    CmdApplicationConfiguration applicationConfiguration;
                    try
                    {
                        applicationConfiguration = serializer.Deserialize<CmdApplicationConfiguration>(jsonReader);
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    yield return applicationConfiguration;
                }
            }
        }
    }
}
