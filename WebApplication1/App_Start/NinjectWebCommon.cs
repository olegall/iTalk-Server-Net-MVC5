[assembly: WebActivator.PreApplicationStartMethod(typeof(WebApplication1.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(WebApplication1.App_Start.NinjectWebCommon), "Stop")]

namespace WebApplication1.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using WebApplication1.Interfaces;
    using WebApplication1.BLL;
    using WebApplication1.DAL;


    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
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
        //private static IKernel CreateKernel()
        //{
        //    var kernel = new StandardKernel();
        //    kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
        //    kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

        //    RegisterServices(kernel);
        //    return kernel;
        //}

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //System.Web.Mvc.DependencyResolver.SetResolver(new WebApplication1.Utils.NinjectDependencyResolver(kernel));

            kernel.Bind<IClientManager>().To<ClientManager>().WithConstructorArgument("rep", Reps.Clients);
            kernel.Bind<ICategoryManager>().To<CategoryManager>().WithConstructorArgument("rep", Reps.Categories)
                                                                 .WithConstructorArgument("categoryImagesRep", Reps.CategoryImages);
            kernel.Bind<ISubcategoryManager>().To<SubcategoryManager>().WithConstructorArgument("rep", Reps.Subcategories);
            kernel.Bind<INotificationManager>().To<NotificationManager>().WithConstructorArgument("rep", Reps.Orders);
        }
    }
}