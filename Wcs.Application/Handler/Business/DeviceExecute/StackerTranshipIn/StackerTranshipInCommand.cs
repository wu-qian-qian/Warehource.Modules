using Common.Application.MediatR.Message;
using Wcs.Device.Device.Tranship;

namespace Wcs.Application.Handler.Business.DeviceExecute.StackerTranshipIn;

public class StackerTranshipInCommand : ICommand
{
    public AbstractStackerTranship InTranShip { get; set; }
}