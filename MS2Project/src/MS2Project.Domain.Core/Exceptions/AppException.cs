
namespace MS2Project.Domain.Core.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public AppResultStatusCode ApiStatusCode { get; set; }
    public object AdditionalData { get; set; }

    public AppException()
        : this(AppResultStatusCode.ServerError)
    {
    }

    public AppException(AppResultStatusCode statusCode)
        : this(statusCode, null)
    {
    }

    public AppException(string message)
        : this(AppResultStatusCode.ServerError, message)
    {
    }

    public AppException(AppResultStatusCode statusCode, string message)
        : this(statusCode, message, HttpStatusCode.InternalServerError)
    {
    }

    public AppException(string message, object additionalData)
        : this(AppResultStatusCode.ServerError, message, additionalData)
    {
    }

    public AppException(AppResultStatusCode statusCode, object additionalData)
        : this(statusCode, null, additionalData)
    {
    }

    public AppException(AppResultStatusCode statusCode, string message, object additionalData)
        : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
    {
    }

    public AppException(AppResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
        : this(statusCode, message, httpStatusCode, null)
    {
    }

    public AppException(AppResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
        : this(statusCode, message, httpStatusCode, null, additionalData)
    {
    }

    public AppException(string message, Exception exception)
        : this(AppResultStatusCode.ServerError, message, exception)
    {
    }

    public AppException(string message, Exception exception, object additionalData)
        : this(AppResultStatusCode.ServerError, message, exception, additionalData)
    {
    }

    public AppException(AppResultStatusCode statusCode, string message, Exception exception)
        : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
    {
    }

    public AppException(AppResultStatusCode statusCode, string message, Exception exception, object additionalData)
        : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
    {
    }

    public AppException(AppResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
        : this(statusCode, message, httpStatusCode, exception, null)
    {
    }

    public AppException(AppResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
        : base(message, exception)
    {
        ApiStatusCode = statusCode;
        HttpStatusCode = httpStatusCode;
        AdditionalData = additionalData;
    }
}

