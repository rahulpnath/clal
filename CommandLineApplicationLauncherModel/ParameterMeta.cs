using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class ParameterMeta<T> where T : IParameter
    {
        public Name Name { get; private set; }
        public Type ParameterType { get; private set; }

        public ParameterMeta(Name name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            this.Name = name;
            this.ParameterType = typeof(T);
        }
    }
}
