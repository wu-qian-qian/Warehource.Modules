using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;

namespace Wcs.Application.Handler.Business.StackerOutComplate;

public class SetExecuteDeviceCommandHandler(IDeviceService _deviceService)
    : ICommandHandler<SetExecuteDeviceCommand, string>
{
    public async Task<string> Handle(SetExecuteDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceName = await _deviceService
            .GetDeviceNameAsync(request.DeviceType, request.Title);
        return deviceName;
    }
}