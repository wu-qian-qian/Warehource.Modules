using Common.Application.Event;
using Common.Application.Log;
using Common.Shared;
using MassTransit;
using MediatR;
using Plc.Application.PlcHandler.Write;
using Plc.CustomEvents;
using Serilog;

namespace Plc.Application.Custom;

/// <summary>
///     2种策略
///     1、分布式缓存存储
///     2、公共事件触发
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
/// <param name="bus"></param>
/// <param name="cache"></param>
internal class WritePlcEventConsumer<TIntegrationEvent>(IMassTransitEventBus bus, ISender sender)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : S7WritePlcDataBlockEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        S7WritePlcDataBlockEvent s7ReadPlcConsumevent = context.Message;
        var readPlcEvent = new WritePlcEventCommand
        {
            UseMemory = s7ReadPlcConsumevent.UseMemory,
            DeviceName = s7ReadPlcConsumevent.DeviceName,
            DBNameToDataValue = s7ReadPlcConsumevent.DBNameToDataValue
        };
        try
        {
            await sender.Send(readPlcEvent);
        }
        catch (Exception e)
        {
            Log.Logger.ForCategory(LogCategory.Event).Information($"{context.Host}--发送Plc读取出现异常{e.Message}");
        }
    }
}