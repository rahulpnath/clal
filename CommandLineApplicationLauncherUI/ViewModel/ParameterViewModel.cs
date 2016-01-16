using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public abstract class ParameterViewModel : ViewModelBase
    {
        public abstract Type GetParameterType();
    }
}
