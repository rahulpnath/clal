﻿using CommandLineApplicationLauncherUI.ViewModel;
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
            Assert.IsAssignableFrom<ViewModelBase>(sut);
        }

        [Theory, AutoData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(NameOnlyParameterViewModel).GetConstructors(System.Reflection.BindingFlags.Public));
        }

        [Theory, AutoData]
        public void CtorParametersAreInitialized(IFixture fixture)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(NameOnlyParameterViewModel).GetConstructors(System.Reflection.BindingFlags.Public));
        }
    }
}
