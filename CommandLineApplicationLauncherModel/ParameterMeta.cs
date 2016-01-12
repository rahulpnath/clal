using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class ParameterMeta
    {
        public Name Name { get; private set; }
        public Type ParameterType { get; private set; }

        public static ParameterMeta Create<T>(Name name) where T : IParameter
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return new ParameterMeta(name, typeof(T));
        }

        private ParameterMeta(Name name, Type type)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            this.Name = name;
            this.ParameterType = type;
        }
    }
}
