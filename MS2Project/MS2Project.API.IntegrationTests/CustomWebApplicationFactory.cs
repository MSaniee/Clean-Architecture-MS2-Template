using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS2Project.Infrastructure.Data.SqlServer.EfCore.Context;
using MS2Project.Infrastructure.Data.SqlServer.EfCore.Tools;
using SqlInMemory;

namespace MS2Project.API.IntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IDisposable
       where TProgram : class
{
    public bool isDataInitialized = false;
    public ApplicationDbContext DbContext { set; get; }
    public IDisposable DbDisposable { get; set; }


 
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                                        d => d.ServiceType ==
                                        typeof(DbContextOptions<ApplicationDbContext>));

            services.Remove(descriptor);

            var connectionString = "Data Source=.;Initial Catalog=TestDb;Integrated Security=true;Trusted_Connection=true;TrustServerCertificate=True";
            DbDisposable = SqlInMemoryDb.Create(connectionString);

            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseSqlServer(connectionString);
                opt.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var appDbContext = serviceProvider.GetService<ApplicationDbContext>();
            DbContext = appDbContext;
            appDbContext.Database.Migrate();
        });

        builder.UseEnvironment("Development");
    }


    public new void Dispose()
    {
        DbDisposable.Dispose();
    }
}