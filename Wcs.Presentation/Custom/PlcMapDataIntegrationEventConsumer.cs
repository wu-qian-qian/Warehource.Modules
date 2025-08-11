using MassTransit;
using MediatR;
using Plc.CustomEvents.Saga;
using Wcs.Application.DBHandler.PlcMap.InsertOrUpdate;
using Wcs.CustomEvents;

namespace Wcs.Presentation.Custom;

public class PlcMapDataIntegrationEventConsumer(ISender sender) : IConsumer<PlcMapDataIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PlcMapDataIntegrationEvent> context)
    {
        PlcMap.PlcMapDataIntegrationCompleted plcMapDataIntegrationCompleted = default;
        //TODO Mediatr 发送事件
        try
        {
            await sender.Send(new InsertOrUpdateEvent
                { DeviceName = context.Message.DeviceName, PlcEntityName = context.Message.PlcEntityName });
            plcMapDataIntegrationCompleted =
                new PlcMap.PlcMapDataIntegrationCompleted(context.Message.DeviceName, true);
        }
        catch (Exception)
        {
            plcMapDataIntegrationCompleted =
                new PlcMap.PlcMapDataIntegrationCompleted(context.Message.DeviceName, false);
        }

        //返回Saga状态
        await context.Publish(plcMapDataIntegrationCompleted);
    }
}