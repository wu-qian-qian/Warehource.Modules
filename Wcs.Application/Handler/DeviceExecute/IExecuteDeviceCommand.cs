using Common.Application.MediatR.Message;
using Wcs.Device.Abstract;

namespace Wcs.Application.Handler.DeviceExecute;

public abstract class IExecuteDeviceCommand : ICommand
{
    protected IExecuteDeviceCommand(IDevice device)
    {
        Device = device;
    }

    public IDevice Device { get; private set; }
}