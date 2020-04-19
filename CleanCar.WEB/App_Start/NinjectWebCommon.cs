[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CleanCar.WEB.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CleanCar.WEB.App_Start.NinjectWebCommon), "Stop")]

namespace CleanCar.WEB.App_Start
{
    using System;
    using System.Web;
    using AutoMapper;
    using CleanCar.DAL.Repositories;
    using CleanCar.DAL.Repositories.IRepositories;
    using CleanCar.Logic.MappingProfiles;
    using CleanCar.Logic.Services;
    using CleanCar.Logic.Services.IServices;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
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
            kernel.Bind<ICustomerService>().To<CustomerService>();
            kernel.Bind<IOperationService>().To<OperationService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IPdfReportService>().To<PdfReportService>();

            kernel.Bind<ICustomerRepository>().To<CustomerRepository>();
            kernel.Bind<IOperationRepository>().To<OperationRepository>();
            kernel.Bind<IOrderRepository>().To<OrderRepository>();

            kernel.Bind<IMapper>().ToMethod(ctx =>
                new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                })))
                .WhenInjectedExactlyInto<CustomerService>();

            kernel.Bind<IMapper>().ToMethod(ctx =>
                new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                })))
                .WhenInjectedExactlyInto<OrderService>();

        }
    }
}