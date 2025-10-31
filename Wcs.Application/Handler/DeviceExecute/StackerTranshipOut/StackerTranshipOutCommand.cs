using Wcs.Device.Abstract;

namespace Wcs.Application.Handler.DeviceExecute.StackerTranshipOut;

public class StackerTranshipOutCommand : IExecuteDeviceCommand
{
    public StackerTranshipOutCommand(IDevice device) : base(device)
    {
    }
}