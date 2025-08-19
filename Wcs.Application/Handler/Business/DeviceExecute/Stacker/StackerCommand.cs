using Common.Application.MediatR.Message;
using Wcs.Device.Device.Stacker;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Business.DeviceExecute.Stacker;

public class StackerCommand : ICommand
{
    public AbstractStacker Stacker { get; set; }
}