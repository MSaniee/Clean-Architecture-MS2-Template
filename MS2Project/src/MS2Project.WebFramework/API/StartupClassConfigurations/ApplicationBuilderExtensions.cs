using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS2Project.Application.Interfaces.DataInitializer;
using MS2Project.Common;

namespace MS2Project.WebFramework.API.StartupClassConfigurations
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHsts(this IApplicationBuilder app, IHostEnvironment env)
        {
            Assert.NotNull(app, nameof(app));
            Assert.NotNull(env, nameof(env));

            if (!env.IsDevelopment())
                app.UseHsts();
        }

        public static void IntializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>(); //Service locator

                //Dos not use Migrations, just Create Database with latest changes
                //dbContext.Database.EnsureCreated();
                //Applies any pending migrations for the context to the database like (Update-Database)
                //dbContext.Database.Migrate();

                var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
                foreach (var dataInitializer in dataInitializers)
                    dataInitializer.InitializeData();
            }
        }
    }
}