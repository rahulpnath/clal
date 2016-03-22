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
        public IEnumerable<CmdApplicationConfigurationParser<string>> StringParsers { get; private set; }

        public CmdApplicationConfigurationViewModelFactory(
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            IEnumerable<CmdApplicationConfigurationParser<string>> stringParsers)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));

            if (stringParsers == null)
                throw new ArgumentNullException(nameof(stringParsers));

            this.Channel = channel;
            this.StringParsers = stringParsers;
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

            return new CmdApplicationConfigurationViewModel(meta, properties, Channel, StringParsers);
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
                    viewModel = new NameValueParameterViewModel(parameterMeta.Name, parameterMeta.DisplayName);
                    viewModel.WithParameter(applicationConfiguration
                         .Parameters
                         .FirstOrDefault(a => a.Name == parameterMeta.Name));
                }
                else if (parameterMeta.ParameterType == typeof(NameOnlyParameter))
                {
                    viewModel = new NameOnlyParameterViewModel(parameterMeta.Name, parameterMeta.DisplayName);
                    viewModel.WithParameter(applicationConfiguration.Parameters.FirstOrDefault(a => a.Name == parameterMeta.Name));
                }
                else
                {
                    throw new ArgumentException(string.Format("Type {0} not supported for parameter {1}", parameterMeta.ParameterType, parameterMeta.Name));
                }

                properties.Add(viewModel);
            }
            var returnValue = new CmdApplicationConfigurationViewModel(
                applicationMeta,
                properties,
                Channel,
                StringParsers);
            returnValue.FriendlyName = (string)applicationConfiguration.Name;
            return returnValue;
        }
    }
}
