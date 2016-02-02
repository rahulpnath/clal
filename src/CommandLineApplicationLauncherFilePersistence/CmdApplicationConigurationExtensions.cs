using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
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

            if(string.IsNullOrEmpty(extension))
                throw new ArgumentNullException(nameof(extension));

            var nameFormat = "{0}-{1}.{2}";
            return string.Format(
                nameFormat,
                applicationConfiguration.ApplicationName,
                FormatFriendlyName(applicationConfiguration.Name),
                extension).ToLower();
        }

        private static string FormatFriendlyName(Name name)
        {
            var spaceReplace = "_";
            return name.ToString().Replace(" ", spaceReplace);
        }
    }
}
