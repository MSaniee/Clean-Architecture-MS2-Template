namespace MS2Project.Domain.Core.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException()
        : base(AppResultStatusCode.NotFound)
    {
    }

    public NotFoundException(string message)
        : base(AppResultStatusCode.NotFound, message)
    {
    }

    public NotFoundException(object additionalData)
        : base(AppResultStatusCode.NotFound, additionalData)
    {
    }

    public NotFoundException(string message, object additionalData)
        : base(AppResultStatusCode.NotFound, message, additionalData)
    {
    }

    public NotFoundException(string message, Exception exception)
        : base(AppResultStatusCode.NotFound, message, exception)
    {
    }

    public NotFoundException(string message, Exception exception, object additionalData)
        : base(AppResultStatusCode.NotFound, message, exception, additionalData)
    {
    }
}

