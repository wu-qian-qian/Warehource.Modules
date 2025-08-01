using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Plc.Application.Abstract;
using Plc.Contracts.Input;
using Plc.Domain.S7;
using Serilog;

namespace Plc.Application.ReadPlc;

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
        netService.WriteAsync(new WriteBufferInput
        {
            
        });
    }
}