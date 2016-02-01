using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using GalaSoft.MvvmLight;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class NameOnlyParameterViewModelTest
    {
        [Theory, AutoData]
        public void SutIsViewModelBase(NameOnlyParameterViewModel sut)
        {
            Assert.IsAssignableFrom<ParameterViewModel>(sut);
            Assert.Equal(typeof(NameOnlyParameter), sut.GetParameterType());
        }

        [Theory, AutoData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(NameOnlyParameterViewModel).GetConstructors());
        }

        [Theory, AutoData]
        public void SutWithoutDisplayNameReturnsEmptyDisplayName(Name aName)
        {
            var sut = new NameOnlyParameterViewModel(aName);
            Assert.Equal(Name.EmptyName, sut.DisplayName);
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(NameOnlyParameterViewModel).GetConstructors());
        }

        [Theory, AutoData]
        public void GetParameterReturnsEmptyIfSelectedFalse(NameOnlyParameterViewModel sut)
        {
            sut.IsSelected = false;
            var parameter = sut.GetParameter();
            Assert.Equal(Maybe.Empty<IParameter>(), parameter);
        }

        [Theory, AutoData]
        public void GetParameterReturnsParameterIfSelected(NameOnlyParameterViewModel sut)
        {
            var expected = Maybe.ToMaybe(new NameOnlyParameter(sut.Name));
            sut.IsSelected = true;
            var actual = sut.GetParameter();
            Assert.Equal(expected, actual);
        }
    }
}
