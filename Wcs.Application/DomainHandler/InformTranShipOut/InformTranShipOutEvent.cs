using Common.Domain.Event;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainHandler.InformTranShipOut;

internal class InformTranShipOutEvent : IEvent
{
    public WcsTask WcsTask { get; set; }
}