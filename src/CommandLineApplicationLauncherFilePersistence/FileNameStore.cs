using CommandLineApplicationLauncherPersistenceModel;
using System.IO;
using CommandLineApplicationLauncherModel;
using System;

namespace CommandLineApplicationLauncherFilePersistence
{
    public class FileNameStore : IStoreWriter<CmdApplicationConfiguration>, IStoreReader<CmdApplicationConfiguration>
    {
        private const string fileExtension  = "json";

        public bool CheckIfFileExists(CmdApplicationConfiguration item)
        {
            var fileName = this.GetConfigurationFileName(item);
            return File.Exists(fileName);
        }

        public Stream OpenStreamFor(CmdApplicationConfiguration item)
        {
            var fileName = this.GetConfigurationFileName(item);
            return File.OpenWrite(fileName);
        }

        // TODO: More cases to be handled
        public string GetConfigurationFileName(CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            var nameFormat = "{0}-{1}.{2}";
            return string.Format(
                nameFormat,
                applicationConfiguration.ApplicationName,
                FormatFriendlyName(applicationConfiguration.Name),
                fileExtension).ToLower();
        }

        private string FormatFriendlyName(Name name)
        {
            var spaceReplace = "_";
            return name.ToString().Replace(" ", spaceReplace);
        }
    }
}
