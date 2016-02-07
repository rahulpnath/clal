using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherViewModel;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
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

        [Theory, AutoData]
        public void SutWithoutDisplayNameReturnsEmptyDisplayName(Name aName)
        {
            var sut = new NameValueParameterViewModel(aName);
            Assert.Equal(Name.EmptyName, sut.DisplayName);
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

        [Theory, AutoData]
        public void WithParameterWithNullValueDoesNotSetIsSelected(NameValueParameterViewModel sut)
        {
            sut.WithParameter(null);
            Assert.True(string.IsNullOrEmpty(sut.Value));
        }

        [Theory, AutoData]
        public void WithParameterWithSameParameterNameSetsIsSelected(
            [Frozen]Name name,
            NameValueParameter parameter,
            NameValueParameterViewModel sut)
        {
            Assert.Equal(parameter.Name, sut.Name);
            sut.WithParameter(parameter);
            Assert.Equal(parameter.Value, sut.Value);
        }

        [Theory, AutoData]
        public void WithParameterWithDiffParameterNameDoesNotSetIsSelected(
            NameValueParameter parameter,
            NameValueParameterViewModel sut)
        {
            Assert.NotEqual(parameter.Name, sut.Name);
            sut.WithParameter(parameter);
            Assert.True(string.IsNullOrEmpty(sut.Value));
        }
    }
}
