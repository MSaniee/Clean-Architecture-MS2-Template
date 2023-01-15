using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MS2Project.Domain.Core.Enums;
using MS2Project.Domain.Core.Exceptions;
using MS2Project.WebFramework.API.Bases;
using Newtonsoft.Json;
//using Serilog;
using System.Net;

namespace MS2Project.WebFramework.API.StartupClassConfigurations.Middlewares;
public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        string message = null;
        string stackTrace = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
        ResultStatus apiStatusCode = ResultStatus.ServerError;

        try
        {
            await _next(context);
        }
        catch (AppException exception)
        {
            //_logger.Error(exception, exception.Message);
            _logger.LogError(exception, exception.Message);
            httpStatusCode = exception.HttpStatusCode;
            apiStatusCode = exception.ApiStatusCode;

            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace,
                };
                if (exception.InnerException != null)
                {
                    dic.Add("InnerException.Exception", exception.InnerException.Message);
                    dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                }
                if (exception.AdditionalData != null)
                    dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));

                message = dic["Exception"];
                stackTrace = JsonConvert.SerializeObject(dic["StackTrace"]);
            }
            else
            {
                message = exception.Message;
            }
            await WriteToResponseAsync();
        }
        catch (SecurityTokenExpiredException exception)
        {
            _logger.LogError(exception, exception.Message);
            SetUnAuthorizeResponse(exception);
            apiStatusCode = ResultStatus.ExpiredToken;
            await WriteToResponseAsync();
        }
        catch (UnauthorizedAccessException exception)
        {
            _logger.LogError(exception, exception.Message);
            SetUnAuthorizeResponse(exception);
            await WriteToResponseAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace,
                };
                message = dic["Exception"];
                stackTrace = JsonConvert.SerializeObject(dic["StackTrace"]);
            }
            await WriteToResponseAsync();
        }

        async Task WriteToResponseAsync()
        {
            if (context.Response.HasStarted)
                throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

            var result = new ApiExceptionResult(false, apiStatusCode, message, stackTrace);
            var json = JsonConvert.SerializeObject(result);

            context.Response.StatusCode = 200; //(int)httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        void SetUnAuthorizeResponse(Exception exception)
        {
            httpStatusCode = HttpStatusCode.Unauthorized;
            apiStatusCode = ResultStatus.UnAuthorized;

            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace
                };
                if (exception is SecurityTokenExpiredException tokenException)
                    dic.Add("Expires", tokenException.Expires.ToString());

                message = JsonConvert.SerializeObject(dic);
            }
        }
    }
}

