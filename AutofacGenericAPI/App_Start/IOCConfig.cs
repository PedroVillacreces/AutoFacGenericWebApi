namespace AutofacGenericAPI
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Http;
    using Model;
    using Repositories;
    using Services;
    using Autofac;
    using Autofac.Integration.WebApi;

    public static class IocConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ProyectoGloboDBEntities>()
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterType<DbService>()
                .As<IDbService>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}