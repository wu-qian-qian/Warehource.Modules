using Common.Application.StateMachine;
using Wcs.Application.Handler.DeviceExecute.StockIn;

namespace Wcs.Application.Handler.DeviceExecute.StockOut;

internal class StockOutCommandHandler : DeviceCommandHandler<StockInCommand>
{
    public StockOutCommandHandler(IStateMachineManager _statemachineManager) : base(_statemachineManager)
    {
    }
}