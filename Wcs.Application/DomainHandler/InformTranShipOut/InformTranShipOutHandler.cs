using Common.Application.Caching;
using Common.Application.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.DeviceController.Tranship.TranshipOut;

namespace Wcs.Application.DomainHandler.InformTranShipOut
{
    internal class InformTranShipOutHandler(IStackerTranshipOutController _controller, ICacheService _cache)
        : IEventHandler<InformTranShipOutEvent>
    {
        public async ValueTask Handle(InformTranShipOutEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var deviceName = _controller.GetDeviceNameByTunnle(domainEvent.WcsTask.GetLocation.GetTunnel);
            var cacheKey = _controller.GetWcsTaskCacheOfKey(deviceName);
            await _cache.SetAsync(cacheKey, domainEvent.WcsTask);
        }
    }
}