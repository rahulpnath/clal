using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherViewModel
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
                    viewModel = new NameValueParameterViewModel(parameterMeta.Name, parameterMeta.DisplayName);
                }
                else if (parameterMeta.ParameterType == typeof(NameOnlyParameter))
                {
                    viewModel = new NameOnlyParameterViewModel(parameterMeta.Name, parameterMeta.DisplayName);
                }
                else
                {
                    throw new ArgumentException(string.Format("Type {0} not supported for parameter {1}", parameterMeta.ParameterType, parameterMeta.Name));
                }

                properties.Add(viewModel);
            }

            return new CmdApplicationConfigurationViewModel(meta.ApplicationName, properties, Channel);
        }

        public CmdApplicationConfigurationViewModel Create(
            CmdApplicationConfiguration applicationConfiguration,
            CmdApplicationMeta applicationMeta)
        {
            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            if (applicationMeta == null)
                throw new ArgumentNullException(nameof(applicationMeta));

            var properties = new List<ParameterViewModel>();
            foreach (var parameterMeta in applicationMeta.ParameterMetas)
            {
                ParameterViewModel viewModel = null;
                if (parameterMeta.ParameterType == typeof(NameValueParameter))
                {
                    var vm = new NameValueParameterViewModel(parameterMeta.Name, parameterMeta.DisplayName);
                    var parameter = (applicationConfiguration
                         .Parameters
                         .FirstOrDefault(a => a.Name == parameterMeta.Name) as NameValueParameter);
                    if (parameter != null)
                        vm.Value = parameter.Value;

                    viewModel = vm;
                }
                else if (parameterMeta.ParameterType == typeof(NameOnlyParameter))
                {
                    var vm = new NameOnlyParameterViewModel(parameterMeta.Name, parameterMeta.DisplayName);
                    vm.IsSelected = applicationConfiguration.Parameters.Any(a => a.Name == parameterMeta.Name);
                    viewModel = vm;
                }
                else
                {
                    throw new ArgumentException(string.Format("Type {0} not supported for parameter {1}", parameterMeta.ParameterType, parameterMeta.Name));
                }

                properties.Add(viewModel);
            }
            var returnValue = new CmdApplicationConfigurationViewModel(
                applicationConfiguration.ApplicationName,
                properties,
                Channel);
            returnValue.FriendlyName = (string)applicationConfiguration.Name;
            return returnValue;
        }
    }
}
