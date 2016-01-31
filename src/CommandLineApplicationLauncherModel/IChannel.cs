namespace CommandLineApplicationLauncherModel
{
    public interface IChannel<T> where T : IMessage
    {
        void Send(T message);
    }
}
