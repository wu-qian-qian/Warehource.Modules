using Common.Application.MediatR.Message;
using Wcs.Device.BaseDevice;

namespace Wcs.Application.Handler.Execute.Business.Stacker;

public class StackerCommand : ICommand
{
    public AbstractStacker Stacker { get; set; }
}