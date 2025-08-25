using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.DeviceExecute.StockOut;

internal class StockOutCommandHandler : ICommandHandler<StockOutCommand>
{
    public Task Handle(StockOutCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}