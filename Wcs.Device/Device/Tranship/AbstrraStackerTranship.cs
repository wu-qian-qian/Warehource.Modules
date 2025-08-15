using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.DeviceConfig;

namespace Wcs.Device.Device.Tranship
{
    public abstract class AbstrraStackerTranship : AbstractDevice<StackerTranShipConfig, PipeLineDBEntity>
    {
        protected AbstrraStackerTranship(string name, StackerTranShipConfig config)
        {
            Name = name;
            Config = config;
        }

        public override string Name { get; init; }
        public override StackerTranShipConfig Config { get; protected set; }

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