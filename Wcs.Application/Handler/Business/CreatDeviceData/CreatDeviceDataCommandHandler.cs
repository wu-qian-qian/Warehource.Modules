using Common.Application.MediatR.Message;
using Common.JsonExtension;
using MediatR;
using Wcs.Application.Handler.DataBase.Device.Get;
using Wcs.Contracts.Respon.Device;
using Wcs.Device.Abstract;
using Wcs.Device.DeviceConfig;
using Wcs.Device.DeviceStructure.Stacker;
using Wcs.Device.DeviceStructure.StockPort;
using Wcs.Device.DeviceStructure.Tranship;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.CreatDeviceData;

internal class CreatDeviceDataCommandHandler(ISender sender) : ICommandHandler<CreatDeviceDataCommand, IDevice[]>
{
    public async Task<IDevice[]> Handle(CreatDeviceDataCommand request, CancellationToken cancellationToken)
    {
        var tempDevice = new List<IDevice>();
        var result = await sender.Send(new GetDeviceQuery
        {
            DeviceType = request.DeviceType,
            Enable = true
        }, cancellationToken);
        if (result.IsSuccess)
        {
            var devices = result.Value.ToArray();

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
            case DeviceTypeEnum.Stacker:
            {
                var config = dto.Config.ParseJson<StackerConfig>();
                device = new Stacker(config, dto);
            }
                break;
            case DeviceTypeEnum.StackerInTranShip:
            {
                var config = dto.Config.ParseJson<StackerTranShipConfig>();
                device = new StackerInTranShip(config, dto);
            }
                break;
            case DeviceTypeEnum.StockPortIn:
            {
                var config = dto.Config.ParseJson<StockPortConfig>();
                device = new StockInPort(config, dto);
            }
                break;
            case DeviceTypeEnum.StockPortOut:
            {
                var config = dto.Config.ParseJson<StockPortConfig>();
                device = new StockOutPort(config, dto);
            }
                break;
            case DeviceTypeEnum.StackerOutTranShip:
            {
                var config = dto.Config.ParseJson<StackerTranShipConfig>();
                device = new StackerOutTranShip(config, dto);
            }
                break;
            default: throw new ArgumentException("不支持添加该类型");
        }

        return device;
    }
}