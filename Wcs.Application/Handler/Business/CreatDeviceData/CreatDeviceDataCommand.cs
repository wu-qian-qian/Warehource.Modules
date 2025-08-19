using Common.Application.MediatR.Message;
using Wcs.Device.Abstract;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.CreatDeviceData;

public class CreatDeviceDataCommand : ICommand<IDevice[]>
{
    public DeviceTypeEnum DeviceType { get; set; }
}