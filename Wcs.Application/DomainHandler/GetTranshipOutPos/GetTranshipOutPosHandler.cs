using Common.Application.Event;
using Wcs.Application.Abstract;
using Wcs.Application.DeviceController.Tranship.TranshipOut;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainHandler.GetTranshipOutPos;

internal sealed class GetTranshipOutPosHandler(
    IStackerTranshipOutController _controller,
    IAnalysisLocation _location) : IEventHandler<GetTranshipOutPosEvent, PutLocation>
{
    public Task<PutLocation> Handle(GetTranshipOutPosEvent @event,
        CancellationToken cancellationToken = default)
    {
        var locationCode = _controller.GetLocationByTunnle(@event.Tunnle);
        var location = _location.AnalysisPutLocation(locationCode);
        return Task.FromResult(location);
    }
}