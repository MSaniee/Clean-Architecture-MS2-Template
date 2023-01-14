using Microsoft.AspNetCore.Http;
using MS2Project.Domain.Core.Exceptions;

namespace MS2Project.WebFramework.API.StartupClassConfigurations.ProblemDetailsService;

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

