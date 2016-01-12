using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class NameValueParameter : IParameter
    {
        public Name Name { get; private set; }
        public string Value { get; private set; }

        public NameValueParameter(Name name, string value)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            this.Name = name;
            this.Value = value;
        }
    }
}
