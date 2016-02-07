using System;

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

        public override bool Equals(object obj)
        {
            var objAsParameter = obj as NameValueParameter;
            if (objAsParameter == null)
                return false;

            return objAsParameter.Name == this.Name && objAsParameter.Value == this.Value;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Value.GetHashCode();
        }

        public string GetValue()
        {
            return this.Name.ToString() + " " + this.Value;
        }
    }
}
