using Common.Application.Event;
using Wcs.Application.DeviceController.Tranship;

namespace Wcs.Application.DomainEvent.ApplyTunnle;

internal class ApplyTunnleHandler(IStackerTranshipInController _controller)
    : IEventHandler<ApplyTunnleEvent, string>
{
    public Task<string> Handle(ApplyTunnleEvent @event, CancellationToken cancellationToken = default)
    {
        var tunnles = _controller.GetTunnleAndPiplineCodeByRegion(@event.RegoinCode);
        var endPos = string.Empty;
        if (tunnles.Any())
            //TODO 发送http请求，或是其他处理获取到合适的通道
            endPos = tunnles.First().Value;

        return Task.FromResult(endPos);
    }
}