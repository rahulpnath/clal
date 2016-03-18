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

            var returnList = new List<IParameter>();

            var serverName = new NameValueParameter((Name)"-S", connectionStringBuilder["Server"] as string);
            var databaseName = connectionStringBuilder["Initial Catalog"] as string;
            if (string.IsNullOrEmpty(databaseName))
                databaseName = "<default>";

            var initialCatalog = new NameValueParameter((Name)"-d", databaseName);

            returnList.Add(serverName);
            returnList.Add(initialCatalog);

            var userId = connectionStringBuilder["user id"] as string;
            var password = connectionStringBuilder["password"] as string;

            IParameter username = null;
            if (!string.IsNullOrEmpty(userId))
            {
                username = new NameValueParameter((Name)"-U", userId);
                returnList.Add(username);
            }

            IParameter pass = null;
            if (!string.IsNullOrEmpty(password))
            {
                pass = new NameValueParameter((Name)"-P", password);
                returnList.Add(pass);
            }

            IParameter integratedSecurity = null;
            if ((bool)connectionStringBuilder["integrated Security"])
            {
                integratedSecurity = new NameOnlyParameter((Name)"-E");
                returnList.Add(integratedSecurity);
            }

            return returnList;
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
