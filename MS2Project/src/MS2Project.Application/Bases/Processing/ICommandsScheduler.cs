namespace $ext_safeprojectname$.Application.Bases.Processing;

public interface ICommandsScheduler
{
    Task EnqueueAsync<T>(ICommand<T> command);
}

