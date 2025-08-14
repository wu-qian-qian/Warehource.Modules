using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Application.DeviceHandler.GetDevice;

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