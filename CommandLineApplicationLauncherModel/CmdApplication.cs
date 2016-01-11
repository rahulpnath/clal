using System;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplication
    {
        public Name FriendlyName { get; private set; }

        public CmdApplication(Name friendlyName)
        {
            if (friendlyName == null)
                throw new ArgumentNullException(nameof(friendlyName));

            this.FriendlyName = friendlyName;
        }

        
    }
}
