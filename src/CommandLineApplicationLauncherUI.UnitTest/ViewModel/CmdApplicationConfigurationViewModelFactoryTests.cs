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
            var viewModel = sut.Create(TestCmdApplicationMeta.Application);
            Assert.Equal(TestCmdApplicationMeta.Application.ApplicationName, viewModel.ApplicationName);
            foreach (var meta in TestCmdApplicationMeta.Application.ParameterMetas)
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

        [Theory]
        [InlineAutoMoqData("friendlyName")]
        public void CreateWithValidCmdApplicationConfigurationReturnsExpected(
            string friendlyName,
            CmdApplicationConfigurationViewModelFactory sut)
        {
            var meta = TestCmdApplicationMeta.Application;
            var configuration = CreateCmdApplicationConfiguration(friendlyName, meta);
            var actual = sut.Create(configuration, meta);
            Assert.Equal(friendlyName, actual.FriendlyName);
            Assert.Equal(meta.ApplicationName, actual.ApplicationName);
        }

        private CmdApplicationConfiguration CreateCmdApplicationConfiguration(
            string friendlyName,
            CmdApplicationMeta meta)
        {
            var parameterList = new List<IParameter>();
            IParameter parameter = new NameOnlyParameter((Name)"-E");
            parameterList.Add(parameter);
            parameter = new NameValueParameter((Name)"-S", "Value");
            parameterList.Add(parameter);
            var configuration = new CmdApplicationConfiguration(
                (Name)friendlyName,
                (Name)meta.ApplicationName,
                new ReadOnlyCollection<IParameter>(parameterList));

            return configuration;
        }
    }
}
