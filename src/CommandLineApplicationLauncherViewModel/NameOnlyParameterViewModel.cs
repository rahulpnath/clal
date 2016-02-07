using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherViewModel
{
    public class NameOnlyParameterViewModel : ParameterViewModel
    {
        public Name Name { get; private set; }

        public Name DisplayName { get; private set; }

        public bool IsSelected { get; set; }

        public NameOnlyParameterViewModel(Name name) : this(name, Name.EmptyName)
        {
        }

        public NameOnlyParameterViewModel(Name name, Name displayName)
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
            return typeof(NameOnlyParameter);
        }

        public override Maybe<IParameter> GetParameter()
        {
            if (!this.IsSelected)
                return Maybe.Empty<IParameter>();

            var parameter = new NameOnlyParameter(this.Name);
            return Maybe.ToMaybe<IParameter>(parameter);
        }

        public override void WithParameter(IParameter parameter)
        {

        }
    }
}
