namespace MS2Project.Application.Bases.Processing;

public interface ICommandsScheduler
{
    Task EnqueueAsync<T>(ICommand<T> command);
}

