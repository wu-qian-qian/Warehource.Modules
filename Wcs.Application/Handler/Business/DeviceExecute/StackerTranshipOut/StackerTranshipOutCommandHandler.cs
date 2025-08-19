using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Business.DeviceExecute.StackerTranshipOut;

internal class StackerTranshipOutCommandHandler : ICommandHandler<StackerTranshipOutCommand>
{
    public Task Handle(StackerTranshipOutCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}