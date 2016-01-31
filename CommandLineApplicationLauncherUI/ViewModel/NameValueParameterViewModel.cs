using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public class NameValueParameterViewModel : ParameterViewModel
    {
        public Name Name { get; private set; }

        public string Value { get; set; }

        public NameValueParameterViewModel(Name name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            this.Name = name;
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
    }
}
