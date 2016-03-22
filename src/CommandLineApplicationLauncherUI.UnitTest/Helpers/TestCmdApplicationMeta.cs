using CommandLineApplicationLauncherModel;
using System.Collections.Generic;

namespace CommandLineApplicationLauncherUI.UnitTest
{
    public static class TestCmdApplicationMeta
    {
        public static CmdApplicationMeta Application
        {
            get
            {
                return new CmdApplicationMeta(
                    (Name)"Sql Server",
                    (Name)"ssms",
                    new List<ParameterMeta>()
                    {
                        ParameterMeta.Create<NameValueParameter>((Name)"-S", (Name)"servername"),
                        ParameterMeta.Create<NameValueParameter>((Name)"-d", (Name)"databasename"),
                        ParameterMeta.Create<NameValueParameter>((Name)"-U", (Name)"username"),
                        ParameterMeta.Create<NameValueParameter>((Name)"-P", (Name)"password"),
                        ParameterMeta.Create<NameOnlyParameter>((Name)"-E", (Name)"windows auth"),
                        ParameterMeta.Create<NameOnlyParameter>((Name)"-nosplash"),
                    });
            }
        }
    }
}
