using CommandLineApplicationLauncherModel;
using System;

namespace CommandLineApplicationLauncherViewModel
{
    public class NameValueParameterViewModel : ParameterViewModel
    {
        public Name Name { get; private set; }

        public Name DisplayName { get; private set; }

        public string Value { get; set; }

        public NameValueParameterViewModel(Name name) : this(name, Name.EmptyName)
        {
        }


        public NameValueParameterViewModel(Name name, Name displayName)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (displayName == null)
                throw new ArgumentNullException(nameof(displayName));

            this.Name = name;
            this.DisplayName = displayName;
        }

        public override Type GetParameterType()
        {
            return typeof(NameValueParameter);
        }

        public override Maybe<IParameter> GetParameter()
        {
            if (string.IsNullOrEmpty(this.Value))
                return Maybe.Empty<IParameter>();

            var parameter = new NameValueParameter(this.Name, this.Value);
            return Maybe.ToMaybe<IParameter>(parameter);
        }

        public override void WithParameter(IParameter parameter)
        {
            var parameterAsNameValue = parameter as NameValueParameter;
            this.Value = parameterAsNameValue != null && parameterAsNameValue.Name == this.Name 
                ? parameterAsNameValue.Value 
                : string.Empty;
        }

        public override Name GetName()
        {
            return this.Name;
        }
    }
}
