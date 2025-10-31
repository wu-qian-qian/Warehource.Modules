using Wcs.Device.Abstract;

namespace Wcs.Application.Handler.DeviceExecute.Stacker;

public class StackerDeviceCommand : IExecuteDeviceCommand
{
    public StackerDeviceCommand(IDevice device) : base(device)
    {
    }
}