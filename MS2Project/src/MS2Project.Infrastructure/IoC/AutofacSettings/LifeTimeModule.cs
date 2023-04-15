using Autofac;
using $ext_safeprojectname$.Application.Dtos;

namespace $ext_safeprojectname$.Infrastructure.IoC.AutofacSettings
{
    public class LifeTimeModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            var domainAssembly = typeof(IRepository<>).Assembly;
            var applicationAssembly = typeof(BaseDto<,,>).Assembly;
            var infrastructureAssembly = typeof(LifeTimeModule).Assembly;

            containerBuilder.RegisterAssemblyTypes(domainAssembly, applicationAssembly, infrastructureAssembly)
                .AssignableTo<IScopedDependency>().AsImplementedInterfaces().InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(domainAssembly, applicationAssembly, infrastructureAssembly)
                .AssignableTo<ITransientDependency>().AsImplementedInterfaces().InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(domainAssembly, applicationAssembly, infrastructureAssembly)
                .AssignableTo<ISingletonDependency>().AsImplementedInterfaces().SingleInstance();
        }

        /*
         Registeration of Autofac :
         for exmaple we have an api project , we shoud add this code in program.cs file :

                public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacModule());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

         */
    }
}
