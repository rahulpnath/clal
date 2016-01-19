using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using GalaSoft.MvvmLight;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class CmdApplicationConfigurationViewModelTests
    {
        [Theory, AutoMoqData]
        public void SutIsViewModelBase(CmdApplicationConfigurationViewModel sut)
        {
            Assert.IsAssignableFrom<ViewModelBase>(sut);
        }

        [Theory, AutoMoqData]
        public void SutHasSaveCommandInitialized(CmdApplicationConfigurationViewModel sut)
        {
            Assert.NotNull(sut.Save);
        }

        [Theory, AutoMoqData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfigurationViewModel).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void CtorParametersAreInitialized(IFixture fixture, Name name)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfigurationViewModel).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void CreateThrowsExcpetionForNullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => CmdApplicationConfigurationViewModel.Create(null));
        }

        [Theory, AutoMoqData]
        public void CreateWithValidParametersReturnsViewModel(
            Name name,
            Name anotherName)
        {
            var viewModel = CmdApplicationConfigurationViewModel.Create(SsmsCmdApplication.Application);
            Assert.Equal(SsmsCmdApplication.Application.ApplicationName, viewModel.ApplicationName);
            foreach (var meta in SsmsCmdApplication.Application.ParameterMetas)
            {
                viewModel.Properties.Any(a => a.GetParameterType() == meta.ParameterType);
            }

        }

        [Theory, AutoMoqData]
        public void CreateWithInvalidParameterTypeThrowsException(
            Name name,
            Name applicationName,
            Name parameterName)
        {
            var parameter = new Mock<IParameter>();
            var meta = new CmdApplicationMeta(
                name,
                applicationName,
                new List<ParameterMeta>()
                {
                    ParameterMeta.Create<IParameter>(parameterName)
                });
            Assert.Throws<ArgumentException>(() => CmdApplicationConfigurationViewModel.Create(meta));
        }
    }
}
