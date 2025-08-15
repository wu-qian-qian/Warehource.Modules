using MassTransit;
using Plc.Application.Abstract;
using Plc.CustomEvents;
using Plc.Domain.S7;

namespace Plc.Presentation.Custom;

public class PlcMapEventCommitConsumer(IS7NetManager netManager, IUnitOfWork unitOfWork)
    : IConsumer<PlcMapEventCommitIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PlcMapEventCommitIntegrationEvent> context)
    {
        var entityItems = await netManager.GetNetWiteDeviceNameAsync(context.Message.DeviceName);
        entityItems.Select(p => p.IsUse = context.Message.Sussce);
        await unitOfWork.SaveChangesAsync();
    }
}