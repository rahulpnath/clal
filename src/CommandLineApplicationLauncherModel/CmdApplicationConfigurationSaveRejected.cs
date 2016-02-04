using System;

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
