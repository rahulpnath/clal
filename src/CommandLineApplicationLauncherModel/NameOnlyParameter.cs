using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class NameOnlyParameter : IParameter
    {
        public Name Name { get; private set; }

        public NameOnlyParameter(Name name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            var objAsParameter = obj as NameOnlyParameter;
            if (objAsParameter == null)
                return false;

            return objAsParameter.Name == this.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public string GetValue()
        {
            return this.Name.ToString();
        }
    }
}
