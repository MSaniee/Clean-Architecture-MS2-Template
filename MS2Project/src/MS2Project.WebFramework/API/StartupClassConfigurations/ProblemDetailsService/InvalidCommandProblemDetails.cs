using Microsoft.AspNetCore.Http;
using $ext_safeprojectname$.Domain.Core.Exceptions;

namespace $ext_safeprojectname$.WebFramework.API.StartupClassConfigurations.ProblemDetailsService;

public class InvalidCommandProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public InvalidCommandProblemDetails(InvalidCommandException exception)
    {
        Title = exception.Message;
        Status = StatusCodes.Status400BadRequest;
        Detail = exception.Details;
        Type = "https://somedomain/validation-error";
    }
}

