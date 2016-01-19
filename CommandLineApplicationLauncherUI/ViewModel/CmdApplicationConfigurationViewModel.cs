using CommandLineApplicationLauncherModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public class CmdApplicationConfigurationViewModel : ViewModelBase
    {
        public Name ApplicationName { get; private set; }

        public string FriendlyName { get; set; }

        public List<ParameterViewModel> Properties { get; private set; }

        public CmdApplicationConfigurationViewModel(Name applicationName, List<ParameterViewModel> properties)
        {
            if (applicationName == null)
                throw new ArgumentNullException(nameof(applicationName));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            this.ApplicationName = applicationName;
            this.Properties = properties;
        }

        public static CmdApplicationConfigurationViewModel Create(
            CmdApplicationMeta meta)
        {
            if (meta == null)
                throw new ArgumentNullException(nameof(meta));

            var properties = new List<ParameterViewModel>();
            foreach (var parameterMeta in meta.ParameterMetas)
            {
                ParameterViewModel viewModel = null;
                if (parameterMeta.ParameterType == typeof(NameValueParameter))
                {
                    viewModel = new NameValueParameterViewModel(parameterMeta.Name);
                }
                else if (parameterMeta.ParameterType == typeof(NameOnlyParameter))
                {
                    viewModel = new NameOnlyParameterViewModel(parameterMeta.Name);
                }
                else
                {
                    throw new ArgumentException(string.Format("Type {0} not supported for parameter {1}", parameterMeta.ParameterType, parameterMeta.Name));
                }

                properties.Add(viewModel);
            }

            return new CmdApplicationConfigurationViewModel(meta.ApplicationName, properties);
        }
    }
}
