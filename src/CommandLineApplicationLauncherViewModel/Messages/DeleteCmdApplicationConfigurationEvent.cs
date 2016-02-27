namespace CommandLineApplicationLauncherViewModel
{
    public class DeleteCmdApplicationConfigurationEvent
    {
        public override bool Equals(object obj)
        {
            var objAsAddCmdApplicationConfiugrationEvent = obj as DeleteCmdApplicationConfigurationEvent;
            if (objAsAddCmdApplicationConfiugrationEvent == null)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}
