using Common.Application.Event;
using Common.Application.QuartzJob;
using Quartz;
using Wcs.Application.Custom;

namespace Wcs.Infrastructure.ReadPlcJob;

[DisallowConcurrentExecution]
internal class ReadPlcJob(IMassTransitEventBus bus) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        Console.WriteLine("启动");
        try
        {
            await bus.PublishAsync(new SendWmsTasksIntegrationEvent(DateTime.Now));
        }
        catch (OperationCanceledException ex)
        {
            throw new JobExecutionException($"任务执行超时，已取消: {ex.Message}", ex, true);
        }

        Console.WriteLine("结束");
    }
}