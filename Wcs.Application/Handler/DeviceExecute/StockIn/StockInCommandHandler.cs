using Common.Application.StateMachine;

namespace Wcs.Application.Handler.DeviceExecute.StockIn;

/// <summary>
/// </summary>
/// <param name="_taskRepository"></param>
/// <param name="_cacheService"></param>
internal class StockInCommandHandler : DeviceCommandHandler<StockInCommand>
{
    public StockInCommandHandler(IStateMachineManager _statemachineManager) : base(_statemachineManager)
    {
    }

    public override Task Handle(StockInCommand request, CancellationToken cancellationToken)
    {
        return base.Handle(request, cancellationToken);
    }
}