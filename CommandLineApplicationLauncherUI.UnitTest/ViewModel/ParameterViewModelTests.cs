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
    public class ParameterViewModelTests
    {
        [Theory, AutoData]
        public void SutIsViewModelBase(ParameterViewModel sut)
        {
            Assert.IsAssignableFrom<ViewModelBase>(sut);
        }
    }
}
