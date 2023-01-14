using Autofac;
using Quartz;
using Quartz.Spi;

namespace MS2Project.Infrastructure.Quartz;

public class JobFactory : IJobFactory
{
    private readonly IContainer _container;

    public JobFactory(IContainer container)
    {
        _container = container;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var job = _container.Resolve(bundle.JobDetail.JobType);

        return job as IJob;
    }

    public void ReturnJob(IJob job)
    {
    }
}