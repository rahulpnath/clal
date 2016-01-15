using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplication
    {
        public Name Name { get; private set; }
        public Name ApplicationName { get; private set; }
        public ReadOnlyCollection<IParameter> Parameters { get; private set; }

        public CmdApplication(Name name, Name applicationName, ReadOnlyCollection<IParameter> parameters)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (applicationName == null)
                throw new ArgumentNullException(nameof(applicationName));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            this.Name = name;
            this.ApplicationName = applicationName;
            this.Parameters = parameters;
        }
    }
}
