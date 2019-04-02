using AuthenticateBLL.Interfaces;
using AuthenticateBLL.Services;
using Microsoft.Owin;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Api.App_Start.NinjectWebCommon), "Stop")]

namespace Api.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using System.Web.Http;
    using Ninject.Web.WebApi;

    using AuthenticationDAL.Interfaces;
    using AuthenticationDAL.Repositories;
    using AuthenticationDAL.Context;
    using AppContext = AuthenticationDAL.Context.AppContext;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(Ninject.Web.Common.WebHost.OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(Ninject.Web.Common.WebHost.NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<IdentityUnitOfWork>();
            kernel.Bind<IAuthenticateService>().To<AuthenticateService>();
            kernel.Bind<IApplicationContext>().To<AppContext>().WithConstructorArgument("DefaultConnection");
            kernel.Bind<IUserService>().To<UserService>();


        }
    }
}