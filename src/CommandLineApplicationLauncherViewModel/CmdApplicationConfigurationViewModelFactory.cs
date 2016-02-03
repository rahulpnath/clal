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
            foreach (var parameter in applicationConfiguration.Parameters)
            {
                if (parameter.GetType() == typeof(NameOnlyParameter))
                {
                    var parameterAsNameOnly = parameter as NameOnlyParameter;
                    var vm = new NameOnlyParameterViewModel(parameterAsNameOnly.Name);
                    vm.IsSelected = true;
                    properties.Add(vm);
                }
                else if (parameter.GetType() == typeof(NameValueParameter))
                {
                    var parameterAsNameValue = parameter as NameValueParameter;
                    var vm = new NameValueParameterViewModel(parameterAsNameValue.Name);
                    vm.Value = parameterAsNameValue.Value;
                    properties.Add(vm);
                }
                else
                {
                    throw new ArgumentException(string.Format("Type {0} not supported for parameter",
                        parameter.GetType().Name));
                }
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
