using Common.Application.QuartzJob;
using MediatR;
using Quartz;

namespace Wcs.Infrastructure.Job;

/// <summary>
///     用来重连
/// </summary>
/// <param name="sender"></param>
/// <param name="netService"></param>
[DisallowConcurrentExecution]
public class ConnectPlcJob(ISender sender) : BaseJob
{
    public override Task Execute(IJobExecutionContext context)
    {
        return base.Execute(context);
    }
}