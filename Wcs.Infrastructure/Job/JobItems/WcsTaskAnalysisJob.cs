using Common.Application.QuartzJob;
using MediatR;
using Quartz;
using Wcs.Application.DeviceHandler.WcsTaskAnalysis;

namespace Wcs.Infrastructure.Job.JobItems;

[DisallowConcurrentExecution]
internal class WcsTaskAnalysisJob(ISender sender) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        try
        {
            await sender.Send(new AnalysisCommandEvent(), _linkedCts.Token);
        }
        catch (OperationCanceledException ex)
        {
            throw new JobExecutionException($"任务执行超时，已取消: {ex.Message}", ex, true);
        }
    }
}