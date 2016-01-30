using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplicationConfigurationSaveRejected : IEvent
    {
        public Guid CorrelationId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Guid MessageId
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
