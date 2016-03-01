namespace CommandLineApplicationLauncherModel
{
    public interface ICommandHandler<T> where T : IMessage
    {
        void Execute(T command);
    }
}
