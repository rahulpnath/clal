using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherViewModel
{
    public abstract class ParameterViewModel : ViewModelBase
    {
        public abstract Type GetParameterType();

        public abstract Maybe<IParameter> GetParameter();
    }
}
