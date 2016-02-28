using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherPersistenceModel;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CommandLineApplicationLauncherJson
{
    public class JsonCmdApplicationConfigurationRepository 
        : ICmdApplicationConfigurationRepository
    {
        private readonly JsonSerializer serializer;
        public IStoreWriter<CmdApplicationConfiguration> FileStoreWriter { get; private set; }
        public IStoreReader<CmdApplicationConfiguration> FileStoreReader { get; private set; }

        public JsonCmdApplicationConfigurationRepository(
            IStoreWriter<CmdApplicationConfiguration> fileStoreWriter,
            IStoreReader<CmdApplicationConfiguration> fileStoreReader)
        {
            if (fileStoreWriter == null)
                throw new ArgumentNullException(nameof(fileStoreWriter));

            if (fileStoreReader == null)
                throw new ArgumentNullException(nameof(fileStoreReader));

            this.FileStoreWriter = fileStoreWriter;
            this.FileStoreReader = fileStoreReader;
            this.serializer = new JsonSerializer();
            this.serializer.Converters.Add(new NameJsonConverter());
            this.serializer.TypeNameHandling = TypeNameHandling.Auto;
            
        }

        public bool CheckIfConfigurationWithSameNameExists(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            return this.FileStoreReader.CheckIfFileExists(applicationConfiguration);
        }

        public void CreateNewConfiguration(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));
            
            using (var stream = this.FileStoreWriter.OpenStreamFor(applicationConfiguration))
            using (var writer = new StreamWriter(stream))
                this.serializer.Serialize(writer, applicationConfiguration);
        }

        public void DeleteConfiguration(CmdApplicationConfiguration applicationConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
