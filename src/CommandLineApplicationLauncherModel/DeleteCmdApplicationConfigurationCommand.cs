using System;

namespace CommandLineApplicationLauncherModel
{
    public class DeleteCmdApplicationConfigurationCommand : IMessage
    {
        public Guid MessageId { get; private set; }

        public CmdApplicationConfiguration ApplicationConfiguration { get; private set; }

        public DeleteCmdApplicationConfigurationCommand(Guid messageId, CmdApplicationConfiguration applicationConfiguration)
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
