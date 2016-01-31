using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class Name
    {
        private string internalName;

        public Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            this.internalName = name;
        }

        public static explicit operator Name(string nameCandidate)
        {
            return new Name(nameCandidate);
        }

        public static explicit operator string(Name name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return name.internalName;
        }

        public static bool operator ==(Name one, Name another)
        {
            return Equals(one, another);
        }

        public static bool operator !=(Name one, Name another)
        {
            return !Equals(one, another);
        }
        public override string ToString()
        {
            return this.internalName;
        }

        public override int GetHashCode()
        {
            return this.internalName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var objAsName = obj as Name;
            if (objAsName == null || this == null)
                return false;

            return this.internalName == objAsName.internalName;
        }
    }
}
