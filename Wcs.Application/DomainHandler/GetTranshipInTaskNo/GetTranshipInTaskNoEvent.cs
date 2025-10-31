using Common.Domain.Event;

namespace Wcs.Application.DomainHandler.GetTranshipInTaskNo;

internal class GetTranshipInTaskNoEvent : IEvent<string>
{
    public string Tunnle { get; set; }
}