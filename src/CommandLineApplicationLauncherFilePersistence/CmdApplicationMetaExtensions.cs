using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherFilePersistence
{
    public static class CmdApplicationMetaExtensions
    {
        public static DirectoryInfo GetConfigurationDirectoryInfo(this CmdApplicationMeta applicationMeta, string rootDirectory)
        {
            if (applicationMeta == null)
                throw new ArgumentNullException(nameof(applicationMeta));

            return new DirectoryInfo(Path.Combine(rootDirectory, applicationMeta.GetConfigurationDirectoryName()))
                .CreateIfDoesNotExists();
        }

        public static string GetConfigurationDirectoryName(this CmdApplicationMeta applicationMeta)
        {
            if (applicationMeta == null)
                throw new ArgumentNullException(nameof(applicationMeta));

            return (string)applicationMeta.ApplicationName;

        }
    }
}
