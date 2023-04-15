namespace $ext_safeprojectname$.Domain.Core.Exceptions;

public class InvalidCommandException : Exception
{
    public string Details { get; }
    public InvalidCommandException(string message, string details) : base(message)
    {
        Details = details;
    }
}
