using Wcs.Device.DeviceStructure.Tranship;

namespace Wcs.Application.Handler.DeviceExecute.StackerTranshipIn;

public class StackerTranshipInCommand : IExecuteDeviceCommand
{
    public StackerTranshipInCommand(AbstractStackerTranship device)
        : base(device.Name, device.Config.DBKey, device.DeviceType, device.DBEntity)
    {
        Device = device;
    }

    public AbstractStackerTranship Device { get; set; }
}