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
        public static CmdApplication Application
        {
            get
            {
                return new CmdApplication(
                    (Name)"Sql Server",
                    (Name)"ssms",
                    new List<Name>());
            }
        }
    }
}
