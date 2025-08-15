using Wcs.Device.Device.StockPort;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.DeviceData;

public class StockOutPort : AbstractStockPort
{
    public StockOutPort(string name, StockPortConfig config) : base(config, name)
    {
    }
}