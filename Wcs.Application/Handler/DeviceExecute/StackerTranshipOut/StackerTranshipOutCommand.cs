using Wcs.Device.DeviceStructure.Tranship;

namespace Wcs.Application.Handler.DeviceExecute.StackerTranshipOut;

public class StackerTranshipOutCommand : IExecuteDeviceCommand
{
    public StackerTranshipOutCommand(AbstractStackerTranship device)
        : base(device.Name, device.Config.DBKey, device.DeviceType, device.DBEntity)
    {
        Device = device;
    }

    public AbstractStackerTranship Device { get; set; }
}