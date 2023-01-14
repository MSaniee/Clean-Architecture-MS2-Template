using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MS2Project.Application.Configurations;
using MS2Project.Application.Dtos;
using MS2Project.Application.Services.Emails;
using MS2Project.Domain.Core.Exceptions;
using MS2Project.Domain.Core.Settings.Site;
using MS2Project.Infrastructure;
using MS2Project.Infrastructure.Caching;
using MS2Project.WebFramework.API.Configuration;
using MS2Project.WebFramework.API.StartupClassConfigurations;
using MS2Project.WebFramework.API.StartupClassConfigurations.Identity;
using MS2Project.WebFramework.API.StartupClassConfigurations.Middlewares;
using MS2Project.WebFramework.API.StartupClassConfigurations.ProblemDetailsService;
using Serilog;
using Serilog.Formatting.Compact;

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
builder.Services.AddProblemDetails(x =>
{
    x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
    x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
});

var assembly = typeof(BaseDto<,>).Assembly;
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddHttpContextAccessor();
ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();

IExecutionContextAccessor executionContextAccessor = new ExecutionContextAccessor(serviceProvider.GetService<IHttpContextAccessor>());

var children = builder.Configuration.GetSection("Caching").GetChildren();
var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
var emailsSettings = builder.Configuration.GetSection("EmailsSettings").Get<EmailsSettings>();
var memoryCache = serviceProvider.GetService<IMemoryCache>();

ApplicationStartup.Initialize(
    builder.Services,
    new MemoryCacheStore(memoryCache, cachingConfiguration),
    emailsSettings,
    ConfigureLogger(),
    executionContextAccessor);

WebApplication app = builder.Build();

app.UseMiddleware<CorrelationMiddleware>();

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

static ILogger ConfigureLogger()
{
    return new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
        .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
        .CreateLogger();
}

public partial class Program { }