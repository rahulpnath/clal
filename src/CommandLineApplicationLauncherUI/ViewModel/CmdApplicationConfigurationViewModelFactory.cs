using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    public class CmdApplicationConfigurationViewModelFactory : ICmdApplicationConfigurationViewModelFactory
    {
        public IChannel<SaveCmdApplicationConfigurationCommand> Channel { get; private set; }

        public CmdApplicationConfigurationViewModelFactory(IChannel<SaveCmdApplicationConfigurationCommand> channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));

            this.Channel = channel;
        }

        public CmdApplicationConfigurationViewModel Create(CmdApplicationMeta meta)
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

            return new CmdApplicationConfigurationViewModel(meta.ApplicationName, properties, Channel);
        }
    }
}
