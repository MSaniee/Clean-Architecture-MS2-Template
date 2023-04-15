
namespace $ext_safeprojectname$.Domain.Core.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public ResultStatus ApiStatusCode { get; set; }
    public object AdditionalData { get; set; }

    public AppException()
        : this(ResultStatus.ServerError)
    {
    }

    public AppException(ResultStatus statusCode)
        : this(statusCode, null)
    {
    }

    public AppException(string message)
        : this(ResultStatus.ServerError, message)
    {
    }

    public AppException(ResultStatus statusCode, string message)
        : this(statusCode, message, HttpStatusCode.InternalServerError)
    {
    }

    public AppException(string message, object additionalData)
        : this(ResultStatus.ServerError, message, additionalData)
    {
    }

    public AppException(ResultStatus statusCode, object additionalData)
        : this(statusCode, null, additionalData)
    {
    }

    public AppException(ResultStatus statusCode, string message, object additionalData)
        : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
    {
    }

    public AppException(ResultStatus statusCode, string message, HttpStatusCode httpStatusCode)
        : this(statusCode, message, httpStatusCode, null)
    {
    }

    public AppException(ResultStatus statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
        : this(statusCode, message, httpStatusCode, null, additionalData)
    {
    }

    public AppException(string message, Exception exception)
        : this(ResultStatus.ServerError, message, exception)
    {
    }

    public AppException(string message, Exception exception, object additionalData)
        : this(ResultStatus.ServerError, message, exception, additionalData)
    {
    }

    public AppException(ResultStatus statusCode, string message, Exception exception)
        : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
    {
    }

    public AppException(ResultStatus statusCode, string message, Exception exception, object additionalData)
        : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
    {
    }

    public AppException(ResultStatus statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
        : this(statusCode, message, httpStatusCode, exception, null)
    {
    }

    public AppException(ResultStatus statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
        : base(message, exception)
    {
        ApiStatusCode = statusCode;
        HttpStatusCode = httpStatusCode;
        AdditionalData = additionalData;
    }
}

