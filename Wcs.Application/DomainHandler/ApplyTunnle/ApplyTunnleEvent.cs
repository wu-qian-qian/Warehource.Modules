using Common.Domain.Event;

namespace Wcs.Application.DomainEvent.ApplyTunnle;

internal class ApplyTunnleEvent : IEvent<string>
{
    public string RegoinCode { get; set; }
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}