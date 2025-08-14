using Common.Application.MediatR.Message;
using Wcs.Application.Abstract.Device.BaseExecute;

namespace Wcs.Application.DeviceHandler.Business.Stacker;

public class StackerCommandEvent : ICommand
{
    public AbstractStacker Stacker { get; set; }
}