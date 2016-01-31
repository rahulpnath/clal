using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherPersistenceModel;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CommandLineApplicationLauncherJson
{
    public class JsonCmdApplicationConfigurationRepository : ICmdApplicationConfigurationRepository
    {
        private readonly JsonSerializer serializer;
        public IStoreWriter<string> FileStoreWriter { get; private set; }
        public IStoreReader<string> FileStoreReader { get; private set; }

        public JsonCmdApplicationConfigurationRepository(
            IStoreWriter<string> fileStoreWriter,
            IStoreReader<string> fileStoreReader)
        {
            if (fileStoreWriter == null)
                throw new ArgumentNullException(nameof(fileStoreWriter));

            if (fileStoreReader == null)
                throw new ArgumentNullException(nameof(fileStoreReader));

            this.FileStoreWriter = fileStoreWriter;
            this.FileStoreReader = fileStoreReader;
            this.serializer = new JsonSerializer();
        }

        public bool CheckIfConfigurationWithSameNameExists(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            var fileName = GetConfigurationFileName(applicationConfiguration);
            return this.FileStoreReader.CheckIfFileExists(fileName);
        }

        public void CreateNewConfiguration(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            var fileName = this.GetConfigurationFileName(applicationConfiguration);
            using (var stream = this.FileStoreWriter.OpenStreamFor(fileName))
            using (var writer = new StreamWriter(stream))
                this.serializer.Serialize(writer, applicationConfiguration);
        }

        // TODO: More cases to be handled
        public string GetConfigurationFileName(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            var nameFormat = "{0}-{1}.json";
            return string.Format(
                nameFormat,
                applicationConfiguration.ApplicationName,
                FormatFriendlyName(applicationConfiguration.Name)).ToLower(); 
        }

        private string FormatFriendlyName(Name name)
        {
            var spaceReplace = "_";
            return name.ToString().Replace(" ", spaceReplace);
        }
    }
}
