using Common.Application.Event;
using Common.Domain.Event;
using MassTransit;
using MediatR;
using Plc.Application.ReadPlc;
using Plc.CustomEvents;

namespace Plc.Application.Custom;

/// <summary>
///     2种策略
///     1、分布式缓存存储
///     2、公共事件触发
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
/// <param name="bus"></param>
/// <param name="cache"></param>
internal class ReadPlcEventConsumer<TIntegrationEvent>(IMassTransitEventBus bus, ISender sender)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IMassTransitDomainEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        if (context.Message.EventHash == nameof(S7ReadPlcDataBlockEvent).GetHashCode())
            if (context.Message is S7ReadPlcDataBlockEvent s7ReadPlcConsumevent)
            {
                var readPlcEvent = new ReadPlcEventCommand
                {
                    UseMemory = s7ReadPlcConsumevent.UserMemory,
                    Ip = s7ReadPlcConsumevent.Ip,
                    DeviceName = s7ReadPlcConsumevent.DeviceName,
                    IsBath = s7ReadPlcConsumevent.IsBath,
                    DBNames = s7ReadPlcConsumevent.DBNames
                };
                await sender.Send(readPlcEvent);
            }
    }
}