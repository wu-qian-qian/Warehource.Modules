using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Business.StockIn;

internal class StockInCommandHandler : ICommandHandler<StockInCommand>
{
    public Task Handle(StockInCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}