using Common.Application.Event;
using Wcs.Application.DeviceController.Tranship;

namespace Wcs.Application.DomainHandler.GetTranshipInTaskNo;

internal class GetTranshipInTaskNoHandler(IStackerTranshipInController _controller)
    : IEventHandler<GetTranshipInTaskNoEvent, string>
{
    public Task<string> Handle(GetTranshipInTaskNoEvent @event, CancellationToken cancellationToken = default)
    {
        var taskNo = _controller.GetWcsTaskNoByTunnle(@event.Tunnle);
        return Task.FromResult(taskNo);
    }
}