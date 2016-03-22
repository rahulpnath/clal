using CommandLineApplicationLauncherViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CommandLineApplicationLauncherUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ApplicationConfigurationList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var senderAsListBox = sender as ListBox;
            if (senderAsListBox != null)
            {
                var context = senderAsListBox.SelectedItem as CmdApplicationConfigurationViewModel;

                if (context != null)
                    context.Launch.Execute(null);
            }
        }
    }
}
