using Autofac;
using MS2Project.Application.Dtos;
using MS2Project.Domain.Core.DILifeTimesType;
using MS2Project.Domain.Interfaces.Repositories;

namespace MS2Project.Infrastructure.IoC.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            //containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            var domainAssembly = typeof(IRepository<>).Assembly;
            var applicationAssembly = typeof(BaseDto<,,>).Assembly;
            var infrastructureAssembly = typeof(AutofacModule).Assembly;

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
