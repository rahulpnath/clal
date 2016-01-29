using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class MainViewModelTests
    {
        [Theory,AutoMoqData]
        public void MainViewModelSetToSsmsApplicationByDefault(IChannel<SaveCmdApplicationConfigurationCommand> channel)
        {
            MainViewModel sut = new MainViewModel(new CmdApplicationConfigurationViewModelFactory(channel));
            Assert.Equal(SsmsCmdApplication.Application.ApplicationName, sut.CmdApplicationConfigurationViewModel.ApplicationName);
        }
    }
}
