using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public class NameOnlyParameterViewModel : ParameterViewModel
    {
        public Name Name { get; private set; }

        public bool IsSelected { get; set; }

        public NameOnlyParameterViewModel(Name name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            this.Name = name;
        }

        public override Type GetParameterType()
        {
            return typeof(NameOnlyParameter);
        }
    }
}
