using Common.Application.MediatR.Message.PageQuery;
using Wcs.Contracts.Respon.Device;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.Device.GetPage;

public class GetDevicePageCommand : PageQuery<DeviceDto>
{
    public DeviceTypeEnum? DeviceType { get; set; }

    public string? DeviceName { get; set; }


    public bool? Enable { get; set; }
}