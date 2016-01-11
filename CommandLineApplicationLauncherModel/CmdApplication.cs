using System;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplication
    {
        public Name FriendlyName { get; private set; }

        public Name ApplicationName { get; private set; }

        public CmdApplication(Name friendlyName, Name applicationName)
        {
            if (friendlyName == null)
                throw new ArgumentNullException(nameof(friendlyName));

            if (applicationName == null)
                throw new ArgumentNullException(nameof(applicationName));

            this.FriendlyName = friendlyName;
            this.ApplicationName = applicationName;
        }

        
    }
}
