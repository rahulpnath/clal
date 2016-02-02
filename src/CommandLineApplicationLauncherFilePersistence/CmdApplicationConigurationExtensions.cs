using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherFilePersistence
{
    public static class CmdApplicationConigurationExtensions
    {
        // TODO: More cases to be handled
        public static string GetFileName(this CmdApplicationConfiguration applicationConfiguration, string extension)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException(nameof(extension));

            var nameFormat = "{0}-{1}.{2}";
            return string.Format(
                nameFormat,
                applicationConfiguration.ApplicationName,
                FormatFriendlyName(applicationConfiguration.Name),
                extension).ToLower();
        }

        public static string GetFilePath(
            this CmdApplicationConfiguration applicationConfiguration,
            string fileExtension,
            string rootDirectory)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            if (fileExtension == null)
                throw new ArgumentNullException(nameof(fileExtension));

            if (rootDirectory == null)
                throw new ArgumentNullException(nameof(rootDirectory));

            var applicationDirectory = applicationConfiguration.GetDirectoryInfo(rootDirectory);
            var fileName = applicationConfiguration.GetFileName(fileExtension);
            var filePath = Path.Combine(applicationDirectory.FullName, fileName);
            return filePath;
        }

        public static DirectoryInfo GetDirectoryInfo(
            this CmdApplicationConfiguration applicationConfiguration,
            string rootDirectory)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            return new DirectoryInfo(Path.Combine(rootDirectory, applicationConfiguration.GetDirectoryName()))
                .CreateIfDoesNotExists();
        }

        public static DirectoryInfo CreateIfDoesNotExists(this DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            return directoryInfo;
        }

        public static string GetDirectoryName(this CmdApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            return (string)applicationConfiguration.ApplicationName;
        }

        private static string FormatFriendlyName(Name name)
        {
            var spaceReplace = "_";
            return name.ToString().Replace(" ", spaceReplace);
        }
    }
}
