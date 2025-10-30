using Common.Application.MediatR.Message;
using MediatR;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DeviceExecute.StockOut;

internal class StockOutCommandHandler(IWcsTaskRepository _wcsTaskRepository, ISender _sender)
    : ICommandHandler<StockOutCommand>
{
    public async Task Handle(StockOutCommand request, CancellationToken cancellationToken)
    {
    }
}