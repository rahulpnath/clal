namespace CommandLineApplicationLauncherModel
{
    public interface IParameter
    {
        Name Name { get;}

        string GetValue();
    }
}