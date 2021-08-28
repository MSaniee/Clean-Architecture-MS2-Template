using Microsoft.AspNetCore.Builder;

namespace MS2Project.WebFramework.API.StartupClassConfigurations.Middlewares
{
    public static class CustomRoutingMiddleware
    {
        public static void UseCustomRouting(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                //endpoints.MapControllers();
                //endpoints.MapControllerRoute(name: "default", "api/v{version:apiVersion=1}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "areas", "api/v{version:apiVersion}/{area:exists}/{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(name: "site", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
