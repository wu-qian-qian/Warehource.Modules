using Common.Application.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Abstract;
using Wcs.Application.DeviceController.Tranship.TranshipOut;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainHandler.GetTranshipOutPos
{
    internal sealed class GetTranshipOutPosHandler(
        IStackerTranshipOutController _controller,
        IAnalysisLocation _location) : IEventHandler<GetTranshipOutPosEvent, PutLocation>
    {
        public ValueTask<PutLocation> Handle(GetTranshipOutPosEvent @event,
            CancellationToken cancellationToken = default)
        {
            var locationCode = _controller.GetLocationByTunnle(@event.Tunnle);
            var location = _location.AnalysisPutLocation(locationCode);
            return ValueTask.FromResult(location);
        }
    }
}