using Common.Application.StateMachine;
using Wcs.Application.Handler.DeviceExecute.StockIn;

namespace Wcs.Application.Handler.DeviceExecute.Stacker;

/// <summary>
///     堆垛机业务处理事件
/// </summary>
/// <param name="_sender"></param>
internal class StackerDeviceCommandHandler : DeviceCommandHandler<StockInCommand>
{
    public StackerDeviceCommandHandler(IStateMachineManager _statemachineManager) : base(_statemachineManager)
    {
    }
}