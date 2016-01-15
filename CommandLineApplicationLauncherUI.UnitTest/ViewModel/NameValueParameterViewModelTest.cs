using CommandLineApplicationLauncherUI.ViewModel;
using GalaSoft.MvvmLight;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class NameValueParameterViewModelTest
    {
        [Theory, AutoData]
        public void SutIsViewModelBase(NameValueParameterViewModel sut)
        {
            Assert.IsAssignableFrom<ViewModelBase>(sut);
        }

        [Theory, AutoData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(NameValueParameterViewModel).GetConstructors(System.Reflection.BindingFlags.Public));
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(NameValueParameterViewModel).GetConstructors(System.Reflection.BindingFlags.Public));
        }
    }
}
