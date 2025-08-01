using Common.Application.Event;
using Common.Application.Log;
using Common.Domain.Event;
using Common.Shared;
using MassTransit;
using MediatR;
using Plc.Application.ReadPlc;
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
internal class ReadPlcEventConsumer<TIntegrationEvent>(ISender sender)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : S7ReadPlcDataBlockEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        S7ReadPlcDataBlockEvent s7ReadPlcConsumevent = context.Message;
        var readPlcEvent = new ReadPlcEventCommand
        {
            UseMemory = s7ReadPlcConsumevent.UseMemory,
            Ip = s7ReadPlcConsumevent.Ip,
            DeviceName = s7ReadPlcConsumevent.DeviceName,
            IsBath = s7ReadPlcConsumevent.IsBath,
            DBNames = s7ReadPlcConsumevent.DBNames,
            Id = s7ReadPlcConsumevent.EventHash
        };
        try
        {
            //3种读取模式   设备单独读取、ip单独读取，自定义变量读取
          await sender.Send(readPlcEvent);
        }
        catch (Exception e)
        {
            Log.Logger.ForCategory(LogCategory.Event).Information($"{context.Host}--发送Plc读取出现异常{e.Message}");
        }
    }
}