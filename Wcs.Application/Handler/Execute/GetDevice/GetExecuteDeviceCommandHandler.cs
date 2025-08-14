using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Application.Handler.Execute.GetDevice;

/// <summary>
///     获取执行设备
/// </summary>
internal class GetExecuteDeviceCommandHandler : ICommandHandler<GetExecuteDeviceCommand, WcsTaskDto>
{
    public Task<WcsTaskDto> Handle(GetExecuteDeviceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}