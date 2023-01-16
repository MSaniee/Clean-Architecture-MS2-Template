using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MS2Project.Domain.Core.Settings.Site;
using MS2Project.WebFramework.API.StartupClassConfigurations;
using MS2Project.WebFramework.API.StartupClassConfigurations.Autufac;
using MS2Project.WebFramework.API.StartupClassConfigurations.Identity;
using MS2Project.WebFramework.API.StartupClassConfigurations.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

SiteSettings siteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

// Add services to the container.
builder.Services.AddConfigureSettings(builder.Configuration);
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddCustomIdentity(siteSettings.IdentitySettings);
builder.Services.AddMinimalMvc();
builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddCustomApiVersioning();
builder.Services.AddSwagger();

//builder.Services.AddHttpContextAccessor();

ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
builder.Host.AddAutofacService(builder.Configuration);

WebApplication app = builder.Build();

//app.UseMiddleware<CorrelationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.IntializeDatabase();

app.UseCustomExceptionHandler();

app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseSwaggerAndUI();

app.UseCustomRouting();

app.Run();

public partial class Program { }