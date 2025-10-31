using Wcs.Device.Abstract;

namespace Wcs.Application.Handler.DeviceExecute.StockIn;

public class StockInCommand : IExecuteDeviceCommand
{
    public StockInCommand(IDevice device) : base(device)
    {
    }
}