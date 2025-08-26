using Common.Application.MediatR.Message;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.StackerOutComplate;

public class SetExecuteDeviceCommand : ICommand<string>
{
    public DeviceTypeEnum DeviceType { get; set; }

    public string Title { get; set; }
}