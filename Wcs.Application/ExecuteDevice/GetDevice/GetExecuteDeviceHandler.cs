using Common.Application.MediatR.Message;
using Wcs.Contracts.WcsTask;

namespace Wcs.Application.GetDevice;

internal class GetExecuteDeviceHandler : ICommandHandler<GetExecuteDeviceEvent, WcsTaskDto>
{
    public Task<WcsTaskDto> Handle(GetExecuteDeviceEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}