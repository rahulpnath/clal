using System.Collections.Generic;
using System.Linq;

namespace CommandLineApplicationLauncherModel
{
    public abstract class CmdApplicationConfigurationParser<T>
    {
        public Maybe<CmdApplicationConfiguration> Parse(T data, CmdApplicationMeta applicationMeta)
        {
            var friendlyName = this.GetFriendlyName(data, applicationMeta);
            var parameters = this.GetParameters(data, applicationMeta);
            if (parameters == null || !parameters.Any())
                return Maybe.Empty<CmdApplicationConfiguration>();

            var configuration = new CmdApplicationConfiguration(
                friendlyName,
                applicationMeta.ApplicationName,
                new System.Collections.ObjectModel.ReadOnlyCollection<IParameter>(parameters)
                );

            return null;
        }

        protected abstract Name GetFriendlyName(T data, CmdApplicationMeta applicationMeta);
        protected abstract IList<IParameter> GetParameters(T data, CmdApplicationMeta applicationMeta);
    }
}
