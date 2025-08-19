using Common.Application.QuartzJob;
using MediatR;
using Quartz;
using Wcs.Application.Handler.Business.WcsTaskAnalysis;

namespace Wcs.Infrastructure.Job.JobItems;

[DisallowConcurrentExecution]
internal class WcsTaskAnalysisJob(ISender sender) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        try
        {
            await sender.Send(new AnalysisCommand(), _linkedCts.Token);
        }
        catch (OperationCanceledException ex)
        {
            throw new JobExecutionException($"任务执行超时，已取消: {ex.Message}", ex, true);
        }
    }
}