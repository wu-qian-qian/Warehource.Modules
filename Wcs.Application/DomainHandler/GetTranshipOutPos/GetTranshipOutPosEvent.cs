using Common.Domain.Event;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainHandler.GetTranshipOutPos;

internal class GetTranshipOutPosEvent : IEvent<PutLocation>
{
    public string Tunnle { get; set; }
}