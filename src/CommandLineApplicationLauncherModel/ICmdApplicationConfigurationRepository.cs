namespace CommandLineApplicationLauncherModel
{
    public interface ICmdApplicationConfigurationRepository
    {
        bool CheckIfConfigurationWithSameNameExists(CmdApplicationConfiguration applicationConfiguration);
        void CreateNewConfiguration(CmdApplicationConfiguration applicationConfiguration);

        void DeleteConfiguration(CmdApplicationConfiguration applicationConfiguration);
    }
}
