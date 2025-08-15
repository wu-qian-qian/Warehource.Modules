using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.JsonExtension;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Handler.DataBase.Device.Get;
using Wcs.Contracts.Respon.Device;
using Wcs.Device.Abstract;
using Wcs.Device.Device.Stacker;
using Wcs.Device.DeviceConfig;
using Wcs.Device.DeviceData;
using Wcs.Domain.Device;

namespace Wcs.Application.Handler.Execute.CreatDeviceData
{
    internal class CreatDeviceDataCommandHandler(ISender sender) : ICommandHandler<CreatDeviceDataCommand, IDevice[]>
    {
        public async Task<IDevice[]> Handle(CreatDeviceDataCommand request, CancellationToken cancellationToken)
        {
            List<IDevice> tempDevice = new List<IDevice>();
            var result = await sender.Send(new GetDeviceQuery
            {
                DeviceType = request.DeviceType,
                Enable = true
            }, cancellationToken);
            if (result.IsSuccess)
            {
                var devices = result.Value.ToArray();
                var tempStacker = new List<AbstractStacker>();
                for (var i = 0; i < devices.Length; i++)
                {
                    var device = devices[i];
                    tempDevice.Add(CreatDevice(device));
                }
            }

            return tempDevice.ToArray();
        }

        public static IDevice CreatDevice(DeviceDto dto)
        {
            IDevice device = default;
            switch (dto.DeviceType)
            {
                case Shared.DeviceTypeEnum.Stacker:
                {
                    var config = dto.Config.ParseJson<StackerConfig>();
                    device = new Stacker(dto.DeviceName, config);
                }
                    break;
                case Shared.DeviceTypeEnum.StackerInTranShip:
                {
                    var config = dto.Config.ParseJson<StackerTranShipConfig>();
                    device = new StackerInTranShip(dto.DeviceName, config);
                }
                    break;
                case Shared.DeviceTypeEnum.StockPortIn:
                {
                    var config = dto.Config.ParseJson<StockPortConfig>();
                    device = new StockInPort(dto.DeviceName, config);
                }
                    break;
                case Shared.DeviceTypeEnum.StockPortOut:
                {
                    var config = dto.Config.ParseJson<StockPortConfig>();
                    device = new StockInPort(dto.DeviceName, config);
                }
                    break;
                case Shared.DeviceTypeEnum.StackerOutTranShip:
                {
                    var config = dto.Config.ParseJson<StackerTranShipConfig>();
                    device = new StackerOutTranShip(dto.DeviceName, config);
                }
                    break;
                default: throw new ArgumentException("不支持添加该类型");
            }

            return device;
        }
    }
}