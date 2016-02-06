using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;

namespace CLALSsmsParser
{
    public class ConnectionStringParser : CmdApplicationConfigurationParser<string>
    {
        protected override Name GetFriendlyName(string data, CmdApplicationMeta applicationMeta)
        {
            throw new NotImplementedException();
        }

        protected override IList<IParameter> GetParameters(string data, CmdApplicationMeta applicationMeta)
        {
            throw new NotImplementedException();
        }
    }
}
