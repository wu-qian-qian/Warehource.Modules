using Wcs.Device.DeviceStructure.StockPort;

namespace Wcs.Application.Handler.DeviceExecute.StockOut;

public class StockOutCommand : IExecuteDeviceCommand
{
    public StockOutCommand(AbstractStockPort device)
        : base(device.Name, device.Config.DBKey, device.DeviceType, device.DBEntity)
    {
        Device = device;
    }

    public AbstractStockPort Device { get; set; }
}