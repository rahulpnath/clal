using CommandLineApplicationLauncherModel;
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
            Assert.IsAssignableFrom<ParameterViewModel>(sut);
            Assert.Equal(typeof(NameValueParameter), sut.GetParameterType());
        }

        [Theory, AutoData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(NameValueParameterViewModel).GetConstructors());
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(NameValueParameterViewModel).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void GetParameterReturnsEmptyIfNoValueIsPresent(NameValueParameterViewModel sut)
        {
            sut.Value = string.Empty;
            var actual = sut.GetParameter();
            Assert.Equal(Maybe.Empty<IParameter>(), actual);
        }

        [Theory, AutoMoqData]
        public void GetParameterReturnsParameterIfValuePresent(NameValueParameterViewModel sut)
        {
            var expected = Maybe.ToMaybe<NameValueParameter>(new NameValueParameter(sut.Name, sut.Value));
            var actual = sut.GetParameter();
            Assert.Equal(expected, actual);
        }
    }
}
