using Common.Application.MediatR.Message;
using Wcs.Contracts.WcsTask;

namespace Wcs.Application.ExecuteDevice.GetDevice;

public class GetExecuteDeviceEvent : ICommand<WcsTaskDto>
{
}