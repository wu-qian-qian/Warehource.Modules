using Wcs.Device.Device.StockPort;
using Wcs.Device.DeviceConfig;
using Wcs.Shared;

namespace Wcs.Device.DeviceData;

public class StockInPort : AbstractStockPort
{
    public StockInPort(StockPortConfig config, string name, string regionCodes, bool enable) : base(config, name,
        regionCodes, enable)
    {
        DeviceType = DeviceTypeEnum.StockPortIn;
    }

    public override bool IsNewStart()
    {
        //TODO 变量判断是否可以下发任务
        return true;
    }
}