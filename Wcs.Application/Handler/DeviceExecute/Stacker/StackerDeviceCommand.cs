using Wcs.Device.Device.Stacker;

namespace Wcs.Application.Handler.DeviceExecute.Stacker;

public class StackerDeviceCommand : IExecuteDeviceCommand
{
    public StackerDeviceCommand(AbstractStacker device) : base(device.Name, device.Config.DBKey, device.DeviceType,
        device.DBEntity)
    {
        Device = device;
    }

    public AbstractStacker Device { get; set; }
}