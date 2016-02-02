/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:CommandLineApplicationLauncherUI"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Autofac;
using CommandLineApplicationLauncherJson;
using CommandLineApplicationLauncherModel;
using CommandLineApplicationLauncherViewModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CommandLineApplicationLauncherUI.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator : IDisposable
    {
        IContainer container;
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var containerBuilder = new ContainerBuilder();
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            var allAssemblies = new List<Assembly>();
            foreach (string dll in Directory.GetFiles(assemblyPath, "*.dll"))
            {
                if (Path.GetFileName(dll).Contains("CommandLineApplicationLauncher"))
                    allAssemblies.Add(Assembly.LoadFile(dll));
            }
            allAssemblies.Add(Assembly.GetExecutingAssembly());

            containerBuilder
                .RegisterAssemblyTypes(allAssemblies.ToArray())
                .Where(a => a.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            containerBuilder.RegisterType<CmdApplicationConfigurationViewModelFactory>().As<ICmdApplicationConfigurationViewModelFactory>();

            containerBuilder.
                RegisterAssemblyTypes(allAssemblies.ToArray())
                .Where(a => a.IsAssignableTo<ViewModelBase>())
                .AsSelf();

            containerBuilder.RegisterType<CmdApplicationConfigurationService>().
                As<ICommandHandler<SaveCmdApplicationConfigurationCommand>>();

            //containerBuilder.RegisterType<CmdApplicationConfigurationViewModel>().
            //    As<IMessageHandler<ConfigurationSavedEvent>>();

            containerBuilder
                .RegisterGeneric(typeof(DirectChannel<>))
                .As(typeof(IChannel<>));

            containerBuilder
                .RegisterAssemblyTypes(allAssemblies.ToArray())
                .AsClosedTypesOf(typeof(ICommandHandler<>));

            containerBuilder
                .RegisterAssemblyTypes(allAssemblies.ToArray())
                .Where(a => a.GetInterfaces().Length > 0)
                .AsImplementedInterfaces();

            container = containerBuilder.Build();
        }

        public MainViewModel Main
        {
            get
            {
                return this.container.Resolve<MainViewModel>();
            }
        }

        public CmdApplicationConfigurationListViewModel CmdApplicationConfigurationListViewModel
        {
            get
            {
                return this.container.Resolve<CmdApplicationConfigurationListViewModel>();
            }
        }

        public CmdApplicationConfigurationViewModel CmdApplicationConfiguration
        {
            get
            {
                return container.Resolve<CmdApplicationConfigurationViewModel>();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    container.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ViewModelLocator() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}