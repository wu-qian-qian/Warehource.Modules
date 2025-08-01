using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.QuartzJob;
using MediatR;
using Plc.CustomEvents;
using Quartz;

namespace Wcs.Infrastructure.Job;

[DisallowConcurrentExecution]
internal class ReadPlcJob(IMassTransitEventBus bus, ISender sender,ICacheService cacheService) : BaseJob
{
    public override async Task Execute(IJobExecutionContext context)
    {
        await base.Execute(context);
        Console.WriteLine("启动");
        try
        {
            //事件触发，最终直接通过读取唯一id
            var guid = Guid.NewGuid();
            await bus.PublishAsync(new S7ReadPlcDataBlockEvent(DateTime.Now)
            {
                Ip="127.0.0.1"
            });
            var buffer =await cacheService.GetAsync("127.0.0.1");
        }
        catch (OperationCanceledException ex)
        {
            throw new JobExecutionException($"任务执行超时，已取消: {ex.Message}", ex, true);
        }

        Console.WriteLine("结束");
    }

    private async Task ReadPlc()
    {
    }
}