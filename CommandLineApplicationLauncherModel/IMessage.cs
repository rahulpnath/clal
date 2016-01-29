using System;

namespace CommandLineApplicationLauncherModel
{
    public interface IMessage
    {
        Guid MessageId { get; }
    }

    public interface ICommand : IMessage
    { }

    public interface IEvent : IMessage
    { }
}
