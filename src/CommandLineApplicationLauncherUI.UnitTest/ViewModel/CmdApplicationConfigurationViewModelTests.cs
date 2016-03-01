using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherUI.ViewModel;
using CommandLineApplicationLauncherViewModel;
using GalaSoft.MvvmLight;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public void GetCmdApplicationConfigurationReturnsEmptyIfFriendlyNameIsEmpty(CmdApplicationConfigurationViewModel sut)
        {
            sut.FriendlyName = string.Empty;
            var actual = sut.GetCmdApplicationConfiguration();
            Assert.Equal(Maybe.Empty<CmdApplicationConfiguration>(), actual);
        }

        [Theory, AutoMoqData]
        public void GetCmdApplicationConfigurationReturnsEmptyIfNoneOfTheParameterHasValue(
            CmdApplicationConfigurationViewModel sut)
        {
            Assert.True(!string.IsNullOrEmpty(sut.FriendlyName));
            Assert.False(sut.Properties.Any(a => a.GetParameter().Any()));
            var actual = sut.GetCmdApplicationConfiguration();
            Assert.False(actual.Any());
        }

        [Theory, AutoMoqData]
        public void GetCmdApplicationConfigurationReturnsConfigurationIfAtleastOneParameterHasValue(
            CmdApplicationConfigurationViewModel sut)
        {
            Assert.True(!string.IsNullOrEmpty(sut.FriendlyName));

            var vm = new NameOnlyParameterViewModel((Name)"testParameter");
            vm.IsSelected = true;
            sut.Properties.Add(vm);

            var actual = sut.GetCmdApplicationConfiguration();
            Assert.True(actual.Any());
        }

        [Theory, AutoMoqData]
        public void EnsureCommandsAreAllSetUpOnSutConstruction(CmdApplicationConfigurationViewModel sut)
        {
            sut.EnsureCommandsAreAllSetUpOnSutConstruction();
        }

        [Theory, AutoMoqData]
        public void SaveCmdApplicationConfigurationCommandNotSendWhenViewModelIsInInvalidState(
            [Frozen]Mock<IChannel<SaveCmdApplicationConfigurationCommand>> channel,
            CmdApplicationConfigurationViewModel sut)
        {
            sut.FriendlyName = string.Empty;
            sut.Save.Execute(null);
            channel.Verify(a => a.Send(It.IsAny<SaveCmdApplicationConfigurationCommand>()), Times.Never());

            sut.FriendlyName = "Valid Friendly Name";
            Assert.False(sut.Properties.Any(a => a.GetParameter().Any()));
            sut.Save.Execute(null);
            channel.Verify(a => a.Send(It.IsAny<SaveCmdApplicationConfigurationCommand>()), Times.Never());
        }

        [Theory, AutoMoqData]
        public void SaveCmdApplicationConfigurationCommandSendWhenViewModelIsInValidState(
            [Frozen]Mock<IChannel<SaveCmdApplicationConfigurationCommand>> channel,
            CmdApplicationConfigurationViewModel sut)
        {
            sut.FriendlyName = "Valid Friendly Name";
            var vm = new NameOnlyParameterViewModel((Name)"testParameter");
            vm.IsSelected = true;
            sut.Properties.Add(vm);

            sut.Save.Execute(null);

            channel.Verify(m => m.Send(It.Is<SaveCmdApplicationConfigurationCommand>(a =>
           a.ApplicationConfiguration.ApplicationName == sut.ApplicationName &&
           a.ApplicationConfiguration.Name == (Name)sut.FriendlyName &&
           a.ApplicationConfiguration.Parameters.Count == 1)), Times.Once());
        }

        [Theory, AutoMoqData]
        public void PopulateFromCmdConfigurationWithNullApplicationThrowsException(CmdApplicationConfigurationViewModel sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.PopulateFromCmdApplicationConfiguration(null));
        }

        [Theory(Skip = "TODO"), AutoMoqData]
        public void PopulateFromCmdConfigurationWithDifferentApplicationDoesNothing(
            IFixture fixture,
            Name applicationName,
            List<NameOnlyParameterViewModel> properties)
        {
            var sut = fixture.WithConstructorParameters(new Dictionary<string, object>()
            {
                {nameof(applicationName), applicationName},
                {nameof(properties), properties.Cast<ParameterViewModel>().ToList() }
            }).Create<CmdApplicationConfigurationViewModel>();
        }
    }

    public static class FixtureExtensions
    {
        public static IFixture WithConstructorParameters(
            this IFixture fixture,
            Dictionary<string, object> parameterNameValues)
        {
            if (fixture == null)
                throw new ArgumentNullException(nameof(fixture));

            if (parameterNameValues == null || !parameterNameValues.Any())
                return fixture;

            fixture.Customizations.Add(new GenericSpecimenBuilder(parameterNameValues));
            return fixture;
        }
    }
    public class GenericSpecimenBuilder : ISpecimenBuilder
    {
        public GenericSpecimenBuilder(Dictionary<string, object> ctorParameters)
        {
            this.CtorParameters = ctorParameters;
        }

        public Dictionary<string, object> CtorParameters { get; private set; }

        public object Create(object request, ISpecimenContext context)
        {
            var requestAsParameterInfo = request as ParameterInfo;
            if (requestAsParameterInfo == null)
                return new NoSpecimen();

            object value;
            if (!this.CtorParameters.TryGetValue(requestAsParameterInfo.Name, out value))
                return new NoSpecimen();

            if (!requestAsParameterInfo.ParameterType.IsAssignableFrom(value.GetType()))
                return new NoSpecimen();

            return value;
        }
    }
}
