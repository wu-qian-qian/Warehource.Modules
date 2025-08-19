using Wcs.Device.Device.StockPort;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceData;

public class StockOutPort : AbstractStockPort
{
    public StockOutPort(StockPortConfig config, string name, string regionCodes, bool enable) : base(config, name,
        regionCodes, enable)
    {
        DeviceType = DeviceTypeEnum.StockPortOut;
    }

    public override bool IsNewStart()
    {
        throw new NotImplementedException();
    }
}