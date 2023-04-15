using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using $ext_safeprojectname$.Application.Mapping;
using $ext_safeprojectname$.Application.Mapping.Profiles;

namespace $ext_safeprojectname$.WebFramework.API.StartupClassConfigurations.AutoMapper;

public static class AutoMapperConfiguration
{
    public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddAutoMapper(config =>
        {
            config.AddCustomMappingProfile();
            config.Advanced.BeforeSeal(configProvicer =>
            {
                configProvicer.CompileMappings();
            });
        }, assemblies);
    }

    public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
    {
        var servicesAssembly = typeof(IHaveCustomMapping).Assembly;

        //این اسمبلی اشاره می کند به لایه اجرایی Assembly.GetEntryAssembly()
        config.AddCustomMappingProfile(Assembly.GetEntryAssembly(), servicesAssembly);
    }

    public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
    {
        var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

        var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
            type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
            .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));

        var profile = new CustomMappingProfile(list);

        config.AddProfile(profile);
    }
}

