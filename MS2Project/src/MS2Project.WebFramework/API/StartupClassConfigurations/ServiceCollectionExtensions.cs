using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using $ext_safeprojectname$.Domain.Core.Settings.Site;
using $ext_safeprojectname$.Infrastructure.Data.SqlServer.EfCore.Context;
using $ext_safeprojectname$.Infrastructure.Data.SqlServer.EfCore.Tools;

namespace $ext_safeprojectname$.WebFramework.API.StartupClassConfigurations;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        
            option.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

        });
    }

    public static void AddConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SiteSettings>(configuration.GetSection(nameof(SiteSettings)));
    }

    public static void AddMinimalMvc(this IServiceCollection services)
    {
        //https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
        services.AddMvcCore(options =>
        {
            options.Filters.Add(new AuthorizeFilter());
            options.EnableEndpointRouting = false;
            //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
            //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
            //Use this filter when use cookie
            //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            //options.UseYeKeModelBinder();
        })
        .AddApiExplorer()
        .AddAuthorization()
        .AddFormatterMappings()
        .AddDataAnnotations()
        //.AddControllersAsServices()
        .AddNewtonsoftJson(options =>
        {
            //options.Formatting = Newtonsoft.Json.Formatting.Indented;
            //options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
            //{
            //    DateTimeFormat = "yyyy/MM/dd HH:mm:ss"
            //};

            options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
            //options.SerializerSettings.Culture = new CultureInfo("en-IE");
            //options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
        })
        .AddCors(
            options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(
                        "https://localhost:44366/",
                        "http://localhost:44366/")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
                });
            }); //.Version_2_1
    }

    public static void AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            //url segment => {version}
            //با دو پراپرتی زیر ورژن دیفالت را تعیین می کنیم
            options.AssumeDefaultVersionWhenUnspecified = true; //default => false;
            options.DefaultApiVersion = new ApiVersion(1, 0); //v1.0 == v1

            //به هدر اضافه میشود و گزارشی از ساپورت ورژن ای پی ای میدهد
            options.ReportApiVersions = true;

            ApiVersion.TryParse("1.0", out var version10);
            ApiVersion.TryParse("1", out var version1);
            var a = version10 == version1;

            //مدلهای مختلف ادرسدهی مثال زده شده است:
            options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            // api/posts?api-version=1

            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            // api/v1/posts

            options.ApiVersionReader = new HeaderApiVersionReader(new[] { "Api-Version" });
            // header => Api-Version : 1

            options.ApiVersionReader = new MediaTypeApiVersionReader();

            //options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new UrlSegmentApiVersionReader())
            // combine of [querystring] & [urlsegment]
        });
    }
}

