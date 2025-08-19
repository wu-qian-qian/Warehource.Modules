using Common.Application.MediatR.Message;
using Common.JsonExtension;
using MediatR;
using Wcs.Application.Handler.DataBase.Device.Get;
using Wcs.Contracts.Respon.Device;
using Wcs.Device.Abstract;
using Wcs.Device.Device.Stacker;
using Wcs.Device.DeviceConfig;
using Wcs.Device.DeviceData;
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
            case DeviceTypeEnum.Stacker:
            {
                var config = dto.Config.ParseJson<StackerConfig>();
                device = new Stacker(dto.DeviceName, config, dto.RegionCode, dto.Enable);
            }
                break;
            case DeviceTypeEnum.StackerInTranShip:
            {
                var config = dto.Config.ParseJson<StackerTranShipConfig>();
                device = new StackerInTranShip(dto.DeviceName, config, dto.RegionCode, dto.Enable);
            }
                break;
            case DeviceTypeEnum.StockPortIn:
            {
                var config = dto.Config.ParseJson<StockPortConfig>();
                device = new StockInPort(config, dto.DeviceName, dto.RegionCode, dto.Enable);
            }
                break;
            case DeviceTypeEnum.StockPortOut:
            {
                var config = dto.Config.ParseJson<StockPortConfig>();
                device = new StockInPort(config, dto.DeviceName, dto.RegionCode, dto.Enable);
            }
                break;
            case DeviceTypeEnum.StackerOutTranShip:
            {
                var config = dto.Config.ParseJson<StackerTranShipConfig>();
                device = new StackerOutTranShip(config, dto.DeviceName, dto.RegionCode, dto.Enable);
            }
                break;
            default: throw new ArgumentException("不支持添加该类型");
        }

        return device;
    }
}