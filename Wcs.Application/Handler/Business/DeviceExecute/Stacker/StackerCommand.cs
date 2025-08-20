using Common.Application.MediatR.Message;
using Wcs.Device.Device.Stacker;

namespace Wcs.Application.Handler.Business.DeviceExecute.Stacker;

public class StackerCommand : ICommand
{
    public AbstractStacker Stacker { get; set; }
}