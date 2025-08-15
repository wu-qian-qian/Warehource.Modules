using Wcs.Device.Device.StockPort;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.DeviceData;

public class StockInPort : AbstractStockPort
{
    public StockInPort(string name, StockPortConfig config) : base(config, name)
    {
    }
}