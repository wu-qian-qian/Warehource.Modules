using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Device;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.Device.Get;

public class GetDeviceQuery : IQuery<Result<IEnumerable<DeviceDto>>>
{
    public DeviceTypeEnum? DeviceType { get; set; }

    public string? DeviceName { get; set; }


    public bool? Enable { get; set; }
}