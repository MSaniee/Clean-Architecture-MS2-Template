namespace $ext_safeprojectname$.Application.Configurations;

public interface IExecutionContextAccessor
{
    Guid CorrelationId { get; }

    bool IsAvailable { get; }
}
