using Common.Application.MediatR.Message;
using Wcs.Contracts.WcsTask;

namespace Wcs.Application.ExecuteDevice.GetDevice;

/// <summary>
///     获取执行设备
/// </summary>
internal class GetExecuteDeviceHandler : ICommandHandler<GetExecuteDeviceEvent, WcsTaskDto>
{
    public Task<WcsTaskDto> Handle(GetExecuteDeviceEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}