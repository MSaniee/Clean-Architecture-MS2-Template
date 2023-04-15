using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace $ext_safeprojectname$.WebFramework.API.StartupClassConfigurations.Swagger;

public static class SwaggerConfigurationExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerExamples();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });

            options.TagActionsBy(api => new[] { api.GroupName });
            options.EnableAnnotations();
            options.ExampleFilters();

            //کد مربوط به اضافه کردن داکیومنت
            var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "$ext_safeprojectname$.API.xml");
            //show controller XML comments like summary
            options.IncludeXmlComments(xmlDocPath, true);

            //options.DescribeAllEnumsAsStrings();

            options.SwaggerDoc("v1", new OpenApiInfo() { Title = "$ext_safeprojectname$-v1", Version = "v1" });
            options.SwaggerDoc("v2", new OpenApiInfo() { Title = "$ext_safeprojectname$-v2", Version = "v2" });

            //Add Multilingual 
            //options.OperationFilter<SwaggerLanguageHeader>();

            #region Versioning

            // Remove version parameter from all Operations
            options.OperationFilter<RemoveVersionParameters>();

            //set version "api/v{version}/[controller]" from current swagger doc verion
            options.DocumentFilter<SetVersionInPaths>();

            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => $"v{v}" == docName);
            });

            options.SchemaFilter<SwaggerIgnoreFilter>();
            options.OperationFilter<IgnorePropertyFilter>();

            #endregion Versioning
        });

        //services.AddSwaggerExamplesFromAssemblyOf(typeof(JTokenRequestExample));
    }

    public static void UseSwaggerAndUI(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.DocExpansion(DocExpansion.None);
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "$ext_safeprojectname$-v1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "$ext_safeprojectname$-v2");
        });
    }
}
