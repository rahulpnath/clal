using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using CommandLineApplicationLauncherViewModel;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace CommandLineApplicationLauncherUI.UnitTest.ViewModel
{
    public class CmdApplicationConfigurationViewModelFactoryTests
    {
        [Theory, AutoMoqData]
        public void CtorWithNullArgumentsThrowsException(IFixture fixture)
        {
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfigurationViewModelFactory).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void CtorParametersAreInitialized(IFixture fixture, Name name)
        {
            var assertion = new ConstructorInitializedMemberAssertion(fixture);
            assertion.Verify(typeof(CmdApplicationConfigurationViewModelFactory).GetConstructors());
        }

        [Theory, AutoMoqData]
        public void CreateThrowsExcpetionForNullParameters(CmdApplicationConfigurationViewModelFactory factory)
        {
            CmdApplicationMeta NullMeta = null;
            Assert.Throws<ArgumentNullException>(() => factory.Create(NullMeta));
        }

        [Theory, AutoMoqData]
        public void CreateWithValidParametersReturnsViewModel(
            Name name,
            Name anotherName,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            CmdApplicationConfigurationViewModelFactory sut)
        {
            var viewModel = sut.Create(SsmsCmdApplication.Application);
            Assert.Equal(SsmsCmdApplication.Application.ApplicationName, viewModel.ApplicationName);
            foreach (var meta in SsmsCmdApplication.Application.ParameterMetas)
            {
                viewModel.Properties.Any(a => a.GetParameterType() == meta.ParameterType);
            }

        }

        [Theory, AutoMoqData]
        public void CreateWithInvalidParameterMetaTypeThrowsException(
            Name name,
            Name applicationName,
            Name parameterName,
            IChannel<SaveCmdApplicationConfigurationCommand> channel,
            CmdApplicationConfigurationViewModelFactory sut)
        {
            var parameter = new Mock<IParameter>();
            var meta = new CmdApplicationMeta(
                name,
                applicationName,
                new List<ParameterMeta>()
                {
                    ParameterMeta.Create<IParameter>(parameterName)
                });
            Assert.Throws<ArgumentException>(() => sut.Create(meta));
        }

        [Theory, AutoMoqData]
        public void CreateWithCmdApplicationConfigurationIfNullThrowsException(
            CmdApplicationConfigurationViewModelFactory sut)
        {
            CmdApplicationConfiguration NullConfiguration = null;
            Assert.Throws<ArgumentNullException>(() => sut.Create(NullConfiguration, SsmsCmdApplication.Application));
        }
        [Theory, AutoMoqData]
        public void CreateWithCmdApplicationConfigurationIfNullMetaThrowsException(
           CmdApplicationConfigurationViewModelFactory sut,
           CmdApplicationConfiguration configuration)
        {
            CmdApplicationMeta NullMeta = null;
            Assert.Throws<ArgumentNullException>(() => sut.Create(configuration, NullMeta));
        }

        [Theory]
        [InlineAutoMoqData("friendlyName", "applicationName")]
        public void CreateWithValidCmdApplicationConfigurationReturnsExpected(
            string friendlyName,
            string applicationName,
            CmdApplicationConfigurationViewModelFactory sut)
        {
            var configuration = CreateCmdApplicationConfiguration(friendlyName, applicationName);
            var actual = sut.Create(configuration, SsmsCmdApplication.Application);
            Assert.Equal(friendlyName, actual.FriendlyName);
            Assert.Equal(applicationName, (string)actual.ApplicationName);
        }

        private CmdApplicationConfiguration CreateCmdApplicationConfiguration(
            string friendlyName,
            string applicationName)
        {
            var parameterList = new List<IParameter>();
            IParameter parameter = new NameOnlyParameter((Name)"NameOnlyParameter");
            parameterList.Add(parameter);
            parameter = new NameValueParameter((Name)"NameValueParameter", "Value");
            parameterList.Add(parameter);
            var configuration = new CmdApplicationConfiguration(
                (Name)friendlyName,
                (Name)applicationName,
                new ReadOnlyCollection<IParameter>(parameterList));

            return configuration;
        }
    }
}
