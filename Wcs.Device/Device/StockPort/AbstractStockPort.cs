using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.Device.StockPort;

public abstract class AbstractStockPort : AbstractDevice<StockPortConfig, PipeLineDBEntity>
{
    protected AbstractStockPort(StockPortConfig config, string name, string regionCodes, bool enable) : base(enable,
        regionCodes)
    {
        Config = config;
        Name = name;
    }

    public override StockPortConfig Config { get; protected set; }

    public override PipeLineDBEntity DBEntity { get; protected set; }


    public override bool CanExecute()
    {
        return DBEntity.RMode == "1" && DBEntity.RErrCode == "0" && Enable;
        ;
    }

    public override bool IsNewStart()
    {
        return DBEntity.RLoad == "1" && DBEntity.RFree == "1";
    }
}