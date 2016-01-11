using System;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplication
    {
        public Name Name { get; private set; }

        public CmdApplication(Name name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            this.Name = name;
        }

        
    }
}
