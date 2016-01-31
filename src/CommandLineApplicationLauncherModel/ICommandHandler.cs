using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public interface ICommandHandler<T> where T : IMessage
    {
        void Execute(T command);
    }
}
