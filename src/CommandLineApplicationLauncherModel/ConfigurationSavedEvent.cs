﻿using System;

namespace CommandLineApplicationLauncherModel
{
    public class ConfigurationSavedEvent : IEvent
    {
        public Guid MessageId { get; private set; }

        public Guid CorrelationId { get; private set; }

        public ConfigurationSavedEvent(Guid commandId)
            : this(Guid.NewGuid(), commandId)
        {
        }

        public ConfigurationSavedEvent(Guid messageId, Guid commandId)
        {
            this.MessageId = messageId;
            this.CorrelationId = commandId;
        }
    }
}
