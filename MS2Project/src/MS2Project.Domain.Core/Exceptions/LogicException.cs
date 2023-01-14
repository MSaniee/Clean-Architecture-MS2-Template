namespace MS2Project.Domain.Core.Exceptions;

public class LogicException : AppException
{
    public LogicException()
        : base(ResultStatus.LogicError)
    {
    }

    public LogicException(string message)
        : base(ResultStatus.LogicError, message)
    {
    }

    public LogicException(object additionalData)
        : base(ResultStatus.LogicError, additionalData)
    {
    }

    public LogicException(string message, object additionalData)
        : base(ResultStatus.LogicError, message, additionalData)
    {
    }

    public LogicException(string message, Exception exception)
        : base(ResultStatus.LogicError, message, exception)
    {
    }

    public LogicException(string message, Exception exception, object additionalData)
        : base(ResultStatus.LogicError, message, exception, additionalData)
    {
    }
}

