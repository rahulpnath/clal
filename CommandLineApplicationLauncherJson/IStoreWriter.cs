using System;
using CommandLineApplicationLauncherModel;
using System.IO;

namespace CommandLineApplicationLauncherJson
{
    public interface IStoreWriter<T> where T : IMessage
    {
        Stream OpenStreamFor(T message);
    }
}