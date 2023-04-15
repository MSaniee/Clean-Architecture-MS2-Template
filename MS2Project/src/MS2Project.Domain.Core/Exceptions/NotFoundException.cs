namespace $ext_safeprojectname$.Domain.Core.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException()
        : base(ResultStatus.NotFound)
    {
    }

    public NotFoundException(string message)
        : base(ResultStatus.NotFound, message)
    {
    }

    public NotFoundException(object additionalData)
        : base(ResultStatus.NotFound, additionalData)
    {
    }

    public NotFoundException(string message, object additionalData)
        : base(ResultStatus.NotFound, message, additionalData)
    {
    }

    public NotFoundException(string message, Exception exception)
        : base(ResultStatus.NotFound, message, exception)
    {
    }

    public NotFoundException(string message, Exception exception, object additionalData)
        : base(ResultStatus.NotFound, message, exception, additionalData)
    {
    }
}

