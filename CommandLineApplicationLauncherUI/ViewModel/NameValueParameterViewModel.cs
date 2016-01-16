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
        public NameValueParameterViewModel(Name name)
        {
        }

        public override Type GetParameterType()
        {
            return typeof(NameValueParameter);
        }
    }
}
