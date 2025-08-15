using Common.Application.MediatR.Message;
using Wcs.Device.DeviceDB;
using Wcs.Shared;

namespace Wcs.Application.Handler.Execute.ReadPlcBlock;

public class GetPlcDBQuery : IQuery<BaseDBEntity>
{
    public string DeviceName { get; set; }

    public DeviceTypeEnum DeviceType { get; set; }

    public string Key { get; set; } 
}