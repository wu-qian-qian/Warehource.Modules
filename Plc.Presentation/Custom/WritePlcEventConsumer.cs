using Common.Application.Log;
using Common.Shared;
using MassTransit;
using MediatR;
using Plc.Application.Handler.ReadWrite.Write;
using Plc.CustomEvents;
using Serilog;
using Wcs.CustomEvents.Saga;

namespace Plc.Presentation.Custom;

/// <summary>
///     2种策略
///     1、分布式缓存存储
///     2、公共事件触发
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
/// <param name="cache"></param>
public class WritePlcEventConsumer<TIntegrationEvent>(ISender sender)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : S7WritePlcDataBlockIntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        WcsWritePlcTaskResult completed = default;
        S7WritePlcDataBlockIntegrationEvent s7ReadPlcConsumevent = context.Message;
        var readPlcEvent = new WritePlcEventCommand
        {
            UseMemory = s7ReadPlcConsumevent.UseMemory,
            DeviceName = s7ReadPlcConsumevent.DeviceName,
            DBNameToDataValue = s7ReadPlcConsumevent.DbNameToDataValue
        };
        try
        {
            var @bool = await sender.Send(readPlcEvent);
            completed = new WcsWritePlcTaskResult(context.Message.DeviceName, @bool, context.Message.Key);
        }
        catch (Exception e)
        {
            Log.Logger.ForCategory(LogCategory.Event).Information($"{context.Host}--发送Plc写入出现异常{e.Message}");
            completed = new WcsWritePlcTaskResult(context.Message.DeviceName, false, context.Message.Key);
        }

        //saga状态机事件
        await context.Publish(completed);
    }
}