namespace HaikuSystem.Web.Api
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using Data;
    using Data.Repositories;
    using Ninject;
    using Ninject.Web.Common;

    public class NinjectConfig
    {
        public static Action<IKernel> DependenciesRegistration = kernel =>
        {
            kernel.Bind<DbContext>().To<HaikuSystemDbContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));
        };

        public static IKernel CreateKernel()
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

        private static void RegisterServices(IKernel kernel)
        {
            DependenciesRegistration(kernel);
        }
    }
}