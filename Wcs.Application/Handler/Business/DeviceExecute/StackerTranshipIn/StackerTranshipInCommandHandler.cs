using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Business.DeviceExecute.StackerTranshipIn;

internal class StackerTranshipInCommandHandler : ICommandHandler<StackerTranshipInCommand>
{
    public Task Handle(StackerTranshipInCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}