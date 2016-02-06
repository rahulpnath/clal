using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CLALSsmsParser
{
    public class ConnectionStringParser : CmdApplicationConfigurationParser<string>
    {
        protected override Name GetFriendlyName(string data, CmdApplicationMeta applicationMeta)
        {
            return null;
        }

        protected override IList<IParameter> GetParameters(string data, CmdApplicationMeta applicationMeta)
        {
            var connectionStringBuilder = GetConnectionStringBuilder(data);
            if (connectionStringBuilder == null)
                return default(IList<IParameter>);

            throw new NotImplementedException();
        }

        private SqlConnectionStringBuilder GetConnectionStringBuilder(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return null;

            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                return builder;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
