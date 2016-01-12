using CommandLineApplicationLauncherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherUI
{
    public static class SsmsCmdApplication
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
                    });
            }
        }
    }
}
