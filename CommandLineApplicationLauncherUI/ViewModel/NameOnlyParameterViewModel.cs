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
        public string Name { get; set; }
        public NameOnlyParameterViewModel(Name name)
        {

        }
    }
}
