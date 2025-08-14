using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Application.DeviceHandler.GetDevice;

public class GetExecuteDeviceEvent : ICommand<WcsTaskDto>
{
}