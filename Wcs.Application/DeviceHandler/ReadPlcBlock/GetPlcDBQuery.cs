using Common.Application.MediatR.Message;
using Wcs.Device.DeviceDB;
using Wcs.Shared;

namespace Wcs.Application.DeviceHandler.ReadPlcBlock;

public class GetPlcDBQuery : IQuery<BaseEntity>
{
    public string DeviceName { get; set; }

    public DeviceTypeEnum DeviceType { get; set; }
}