using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class ConfigurationSavedEvent : IMessage
    {
        public Guid MessageId { get; private set; }

        public ConfigurationSavedEvent(Guid messageId)
        {
            this.MessageId = messageId;
        }
    }
}
