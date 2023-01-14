using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2Project.Domain.Core.Exceptions;

namespace MS2Project.WebFramework.API.StartupClassConfigurations.ProblemDetailsService;

public class BusinessRuleValidationExceptionProblemDetails : ProblemDetails
{
    public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
    {
        Title = "Business rule validation error";
        Status = StatusCodes.Status409Conflict;
        Detail = exception.Details;
        Type = "https://somedomain/business-rule-validation-error";
    }
}

