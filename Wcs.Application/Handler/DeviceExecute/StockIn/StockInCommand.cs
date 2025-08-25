using Wcs.Device.Device.StockPort;

namespace Wcs.Application.Handler.DeviceExecute.StockIn;

public class StockInCommand : IExecuteDeviceCommand
{
    public StockInCommand(AbstractStockPort device)
        : base(device.Name, device.Config.DBKey, device.DeviceType, device.DBEntity)
    {
        Device = device;
    }

    public AbstractStockPort Device { get; set; }
}