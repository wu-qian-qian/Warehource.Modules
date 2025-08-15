using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.Device.StockPort
{
    public abstract class AbstractStockPort : AbstractDevice<StockPortConfig, PipeLineDBEntity>
    {
        protected AbstractStockPort(StockPortConfig config, string name)
        {
            Config = config;
            Name = name;
        }

        public override StockPortConfig Config { get; protected set; }

        public override string Name { get; init; }
        public override PipeLineDBEntity DBEntity { get; protected set; }


        public override bool CanExecute()
        {
            throw new NotImplementedException();
        }

        public override bool IsNewStart()
        {
            throw new NotImplementedException();
        }
    }
}