using Wcs.Device.Abstract;

namespace Wcs.Application.Handler.DeviceExecute.StackerTranshipIn;

public class StackerTranshipInCommand : IExecuteDeviceCommand
{
    public StackerTranshipInCommand(IDevice device) : base(device)
    {
    }
}