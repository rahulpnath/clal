using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public class SaveCmdApplicationConfigurationCommand : ICommand
    {
        public Guid MessageId { get; private set; }

        public CmdApplicationConfiguration ApplicationConfiguration { get; private set; }

        public SaveCmdApplicationConfigurationCommand(Guid messageId, CmdApplicationConfiguration applicationConfiguration)
        {
            if (messageId == null || messageId == Guid.Empty)
                throw new ArgumentNullException(nameof(messageId));

            if (applicationConfiguration == null)
                throw new ArgumentNullException(nameof(applicationConfiguration));

            this.MessageId = messageId;
            this.ApplicationConfiguration = applicationConfiguration;
        }
    }
}
