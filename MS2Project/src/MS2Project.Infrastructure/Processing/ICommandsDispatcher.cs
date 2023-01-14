namespace MS2Project.Infrastructure.Processing;

public interface ICommandsDispatcher
{
    Task DispatchCommandAsync(Guid id);
}
