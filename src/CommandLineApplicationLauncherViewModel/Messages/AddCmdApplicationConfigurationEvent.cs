namespace CommandLineApplicationLauncherViewModel
{
    public class AddCmdApplicationConfigurationEvent 
    {
        public override bool Equals(object obj)
        {
            var objAsAddCmdApplicationConfiugrationEvent = obj as AddCmdApplicationConfigurationEvent;
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
