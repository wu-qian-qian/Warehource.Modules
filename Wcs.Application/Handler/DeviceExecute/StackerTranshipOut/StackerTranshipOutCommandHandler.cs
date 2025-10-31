using Common.Application.StateMachine;
using Wcs.Application.Handler.DeviceExecute.StockIn;

namespace Wcs.Application.Handler.DeviceExecute.StackerTranshipOut;

internal class StackerTranshipOutCommandHandler : DeviceCommandHandler<StockInCommand>
{
    public StackerTranshipOutCommandHandler(IStateMachineManager _statemachineManager) : base(_statemachineManager)
    {
    }
}