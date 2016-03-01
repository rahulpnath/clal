using System;

namespace CommandLineApplicationLauncherModel
{
    public class ConfigurationDeletedEvent : IEvent
    {
        public Guid MessageId { get; private set; }

        public Guid CorrelationId { get; private set; }

        public ConfigurationDeletedEvent(Guid commandId)
            : this(Guid.NewGuid(), commandId)
        {
        }

        public ConfigurationDeletedEvent(Guid messageId, Guid commandId)
        {
            this.MessageId = messageId;
            this.CorrelationId = commandId;
        }
    }
}
