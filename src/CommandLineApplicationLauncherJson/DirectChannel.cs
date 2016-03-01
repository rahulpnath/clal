using CommandLineApplicationLauncherModel;
using System.Collections.Generic;

namespace CommandLineApplicationLauncherJson
{
    public class DirectChannel<T> : IChannel<T> where T : ICommand
    {
        public IEnumerable<ICommandHandler<T>> CommandHandlers { get; private set; }

        public DirectChannel(IEnumerable<ICommandHandler<T>> commandHandler)
        {
            this.CommandHandlers = commandHandler;
        }

        public void Send(T message)
        {
            foreach (var commandHandler in this.CommandHandlers)
                commandHandler.Execute(message);
        }
    }
}
