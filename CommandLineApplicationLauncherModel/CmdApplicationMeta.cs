using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplicationMeta
    {
        public Name FriendlyName { get; private set; }
        public Name ApplicationName { get; private set; }
        public ReadOnlyCollection<ParameterMeta> ParameterMetas { get; set; }

        public CmdApplicationMeta(Name friendlyName, Name applicationName, IEnumerable<ParameterMeta> parameterMetas)
        {
            if (friendlyName == null)
                throw new ArgumentNullException(nameof(friendlyName));

            if (applicationName == null)
                throw new ArgumentNullException(nameof(applicationName));

            if (parameterMetas == null)
                throw new ArgumentNullException(nameof(parameterMetas));

            this.FriendlyName = friendlyName;
            this.ApplicationName = applicationName;
            this.ParameterMetas = new ReadOnlyCollection<ParameterMeta>(new List<ParameterMeta>(parameterMetas));
        }

        
    }
}
