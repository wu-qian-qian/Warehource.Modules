using Common.Shared;

namespace Plc.Contracts.Request;

public class GetEntityItemPageRequest : PagingQuery
{
    public string? Ip { get; set; }

    public string? DeviceName { get; set; }

    public string? Name { get; set; }
}