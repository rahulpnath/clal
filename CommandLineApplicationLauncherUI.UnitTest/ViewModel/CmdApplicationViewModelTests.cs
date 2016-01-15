using CommandLineApplicationLauncherUI.ViewModel;
using GalaSoft.MvvmLight;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class CmdApplicationViewModelTests
    {

        [Theory, AutoData]
        public void SutIsViewModelBase(CmdApplicationViewModel sut)
        {
            Assert.IsAssignableFrom<ViewModelBase>(sut);
        }
    }
}
