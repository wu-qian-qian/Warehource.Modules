using MassTransit;
using MediatR;
using Plc.CustomEvents.Saga;
using Wcs.Application.Handler.DB.PlcMap.InsertOrUpdate;
using Wcs.CustomEvents;

namespace Wcs.Presentation.Custom;

public class PlcMapDataIntegrationEventConsumer(ISender sender) : IConsumer<PlcMapDataIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PlcMapDataIntegrationEvent> context)
    {
        PlcMapDataIntegrationCompleted plcMapDataIntegrationCompleted = default;
        //TODO Mediatr 发送事件
        try
        {
            await sender.Send(new InsertOrUpdateCommand
                { DeviceName = context.Message.DeviceName, PlcEntityName = context.Message.PlcEntityName });
            //Saga 状态
            plcMapDataIntegrationCompleted =
                new PlcMapDataIntegrationCompleted(context.Message.DeviceName, true);
        }
        catch (Exception)
        {
            //Saga 状态
            plcMapDataIntegrationCompleted =
                new PlcMapDataIntegrationCompleted(context.Message.DeviceName, false);
        }

        //返回Saga状态
        await context.Publish(plcMapDataIntegrationCompleted);
    }
}