namespace CommandLineApplicationLauncherViewModel
{
    public interface IReader<in T, out TResult>
    {
        TResult Query(T criteria);
    }
}
