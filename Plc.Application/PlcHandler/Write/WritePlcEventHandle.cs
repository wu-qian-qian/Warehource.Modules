using Common.Application.MediatR.Message;
using Plc.Application.Abstract;
using Plc.Contracts.Input;
using Plc.Domain.S7;

namespace Plc.Application.PlcHandler.Write;

/// <summary>
///     最终处理者
/// </summary>
/// <param name="service"></param>
internal class WritelPlcEventHandle(INetService netService, IS7NetManager netManager)
    : ICommandHandler<WritePlcEventCommand>
{
    /// <summary>
    ///   
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(WritePlcEventCommand request, CancellationToken cancellationToken)
    {
        var dbName = request.DBNameToDataValue.Keys.ToArray();
        var s7EntityItems =await netManager
            .GetDeviceNameWithDBNameAsync(request.DeviceName, dbName.ToList());
        var S7EntityItemGroup = s7EntityItems.GroupBy(p => p.Ip);
        foreach (var item in request.writeBufferInputs)
        {
            await netService.WriteAsync(item);
        }
    }
}