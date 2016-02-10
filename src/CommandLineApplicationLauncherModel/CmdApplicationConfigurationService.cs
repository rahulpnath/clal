using System;

namespace CommandLineApplicationLauncherModel
{
    public class CmdApplicationConfigurationService : ICommandHandler<SaveCmdApplicationConfigurationCommand>
    {
        public ICmdApplicationConfigurationRepository Repository { get; private set; }

        public CmdApplicationConfigurationService(ICmdApplicationConfigurationRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            this.Repository = repository;
        }

        public void Execute(SaveCmdApplicationConfigurationCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (this.Repository.CheckIfConfigurationWithSameNameExists(command.ApplicationConfiguration))
                DomainEvents.Publish(new CmdApplicationConfigurationSaveRejected());

            this.Repository.CreateNewConfiguration(command.ApplicationConfiguration);
            DomainEvents.Publish(new ConfigurationSavedEvent(Guid.NewGuid(), command.MessageId));
        }
    }
}
