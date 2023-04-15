namespace $ext_safeprojectname$.Infrastructure.Processing;

public interface IDomainEventsDispatcher
{
    Task DispatchEventsAsync();
}

