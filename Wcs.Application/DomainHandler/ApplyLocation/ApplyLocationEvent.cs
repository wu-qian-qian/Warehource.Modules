using Common.Domain.Event;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainHandler.ApplyLocation;

internal class ApplyLocationEvent : IEvent<PutLocation>
{
    public string Tunnle { get; set; }
}