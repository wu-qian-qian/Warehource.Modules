using Common.Application.Event;
using MassTransit;
using NPOI.OpenXmlFormats.Vml;
using Wcs.Application.DomainEvent.WritePlcData;
using Wcs.CustomEvents;

namespace Wcs.Presentation.Custom;

/// <summary>
///     状态机更新状态如果写入数据失败，状态机任务状态，重新写入数据
/// </summary>
/// <param name="send"></param>
public class WcsWritePlcTaskDataConsumer(IEventBus _bus) : IConsumer<WcsWritePlcTaskDataIntegrationEvent>
{
    public async Task Consume(ConsumeContext<WcsWritePlcTaskDataIntegrationEvent> context)
    {
        await _bus.PublishAsync(new WritePlcDataEvent
        {
            CacheKey = context.Message.CacheKey,
            IsSuccess = context.Message.IsSucess
        });
    }
}