using Common.Application.MediatR.Message;
using Wcs.Device.BaseDevice;

namespace Wcs.Application.DeviceHandler.Business.Stacker;

public class StackerCommandEvent : ICommand
{
    public AbstractStacker Stacker { get; set; }
}