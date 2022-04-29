namespace MS2Project.Domain.Core.Exceptions;

public class LogicException : AppException
{
    public LogicException()
        : base(AppResultStatusCode.LogicError)
    {
    }

    public LogicException(string message)
        : base(AppResultStatusCode.LogicError, message)
    {
    }

    public LogicException(object additionalData)
        : base(AppResultStatusCode.LogicError, additionalData)
    {
    }

    public LogicException(string message, object additionalData)
        : base(AppResultStatusCode.LogicError, message, additionalData)
    {
    }

    public LogicException(string message, Exception exception)
        : base(AppResultStatusCode.LogicError, message, exception)
    {
    }

    public LogicException(string message, Exception exception, object additionalData)
        : base(AppResultStatusCode.LogicError, message, exception, additionalData)
    {
    }
}

