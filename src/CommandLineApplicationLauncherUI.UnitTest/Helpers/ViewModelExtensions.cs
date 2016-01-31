using GalaSoft.MvvmLight;
using System.Linq;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest
{
    public static class ViewModelExtensions
    {
        public static void EnsureCommandsAreAllSetUpOnSutConstruction(this ViewModelBase sut)
        {
            foreach (var property in
                sut.GetType().GetProperties()
                .Where(a => a.PropertyType.IsAssignableFrom(typeof(System.Windows.Input.ICommand))))
            {
                var command = property.GetValue(sut) as System.Windows.Input.ICommand;
                Assert.NotNull(command);
            }
        }
    }
}
