using Common.Application.StateMachine;
using Wcs.Application.Handler.DeviceExecute.StockIn;

namespace Wcs.Application.Handler.DeviceExecute.StackerTranshipIn;

internal class StackerTranshipInCommandHandler : DeviceCommandHandler<StockInCommand>
{
    public StackerTranshipInCommandHandler(IStateMachineManager _statemachineManager) : base(_statemachineManager)
    {
    }
}