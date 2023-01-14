using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MS2Project.Domain.Core.Settings.Site;
using MS2Project.Domain.UserAggregate;
using MS2Project.Domain.UserAggregate.Roles;
using MS2Project.Infrastructure.Data.SqlServer.EfCore.Context;

namespace MS2Project.WebFramework.API.StartupClassConfigurations.Identity;

public static class IdentityConfigurationExtensions
{
    public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings identitySettings)
    {
        services.AddIdentity<User, Role>(identityOptions =>
        {
            //تنظیمات پسورد
            identityOptions.Password.RequireDigit = identitySettings.PasswordRequireDigit;
            identityOptions.Password.RequiredLength = identitySettings.PasswordRequiredLength;
            identityOptions.Password.RequireNonAlphanumeric = identitySettings.PasswordRequireNonAlphanumic;
            identityOptions.Password.RequireUppercase = identitySettings.PasswordRequireUppercase;
            identityOptions.Password.RequireLowercase = identitySettings.PasswordRequireLowercase;

            //کاربر
            identityOptions.User.RequireUniqueEmail = identitySettings.RequireUniqueEmail;

            //Singin Settings
            //identityOptions.SignIn.RequireConfirmedEmail = false;
            //identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

            //Lockout Settings
            //identityOptions.Lockout.MaxFailedAccessAttempts = 5;
            //identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //identityOptions.Lockout.AllowedForNewUsers = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    }
}
