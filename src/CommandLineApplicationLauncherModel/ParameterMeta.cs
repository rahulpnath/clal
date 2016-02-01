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
        public Name DisplayName { get; set; }

        public static ParameterMeta Create<T>(Name name, Name displayName = null) where T : IParameter
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (displayName == null)
                displayName = Name.EmptyName;

            return new ParameterMeta(name, typeof(T), displayName);
        }

        private ParameterMeta(Name name, Type type, Name displayName)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (displayName == null)
                throw new ArgumentNullException(nameof(displayName));

            this.Name = name;
            this.ParameterType = type;
            this.DisplayName = displayName;
        }
    }
}
