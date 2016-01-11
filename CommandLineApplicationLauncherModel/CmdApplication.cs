using System;
using System.Collections.Generic;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplication
    {
        public Name FriendlyName { get; private set; }
        public Name ApplicationName { get; private set; }
        public IEnumerable<Name> ParameterNames { get; set; }

        public CmdApplication(Name friendlyName, Name applicationName, IEnumerable<Name> parameterNames)
        {
            if (friendlyName == null)
                throw new ArgumentNullException(nameof(friendlyName));

            if (applicationName == null)
                throw new ArgumentNullException(nameof(applicationName));

            if (parameterNames == null)
                throw new ArgumentNullException(nameof(parameterNames));

            this.FriendlyName = friendlyName;
            this.ApplicationName = applicationName;
            this.ParameterNames = parameterNames;
        }

        
    }
}
