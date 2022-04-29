namespace MS2Project.Domain.Core.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException()
        : base(AppResultStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message)
        : base(AppResultStatusCode.BadRequest, message)
    {
    }

    public BadRequestException(object additionalData)
        : base(AppResultStatusCode.BadRequest, additionalData)
    {
    }

    public BadRequestException(string message, object additionalData)
        : base(AppResultStatusCode.BadRequest, message, additionalData)
    {
    }

    public BadRequestException(string message, Exception exception)
        : base(AppResultStatusCode.BadRequest, message, exception)
    {
    }

    public BadRequestException(string message, Exception exception, object additionalData)
        : base(AppResultStatusCode.BadRequest, message, exception, additionalData)
    {
    }
}

