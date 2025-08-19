using Common.Application.MediatR.Message;
using Wcs.Device.Device.Tranship;

namespace Wcs.Application.Handler.Business.DeviceExecute.StackerTranshipOut;

public class StackerTranshipOutCommand : ICommand
{
    public AbstractStackerTranship OutTranShip { get; set; }
}