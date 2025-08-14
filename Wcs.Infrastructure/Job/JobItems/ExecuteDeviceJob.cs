using Common.Application.QuartzJob;
using MediatR;
using Quartz;

namespace Wcs.Infrastructure.Job;

[DisallowConcurrentExecution]
internal class ExecuteDeviceJob(ISender sender) : BaseJob
{
    public override Task Execute(IJobExecutionContext context)
    {
        return base.Execute(context);
    }
}