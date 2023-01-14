namespace MS2Project.Domain.Core.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException()
        : base(ResultStatus.BadRequest)
    {
    }

    public BadRequestException(string message)
        : base(ResultStatus.BadRequest, message)
    {
    }

    public BadRequestException(object additionalData)
        : base(ResultStatus.BadRequest, additionalData)
    {
    }

    public BadRequestException(string message, object additionalData)
        : base(ResultStatus.BadRequest, message, additionalData)
    {
    }

    public BadRequestException(string message, Exception exception)
        : base(ResultStatus.BadRequest, message, exception)
    {
    }

    public BadRequestException(string message, Exception exception, object additionalData)
        : base(ResultStatus.BadRequest, message, exception, additionalData)
    {
    }
}

