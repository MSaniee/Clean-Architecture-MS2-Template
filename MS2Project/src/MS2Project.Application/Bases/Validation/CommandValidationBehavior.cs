using FluentValidation;
using $ext_safeprojectname$.Domain.Core.Exceptions;

namespace $ext_safeprojectname$.Application.Bases.Validation;

public class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IList<IValidator<TRequest>> _validators;

    public CommandValidationBehavior(IList<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var errors = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (errors.Any())
        {
            var errorBuilder = new StringBuilder();

            errorBuilder.AppendLine("Invalid command, reason: ");

            foreach (var error in errors)
            {
                errorBuilder.AppendLine(error.ErrorMessage);
            }

            throw new InvalidCommandException(errorBuilder.ToString(), null);
        }

        return next();
    }
}

