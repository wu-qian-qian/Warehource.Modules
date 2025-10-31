using Wcs.Device.Abstract;

namespace Wcs.Application.Handler.DeviceExecute.StockOut;

public class StockOutCommand : IExecuteDeviceCommand
{
    public StockOutCommand(IDevice device) : base(device)
    {
    }
}