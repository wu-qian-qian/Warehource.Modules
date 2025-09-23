using Common.Shared;
using Plc.Shared;

namespace Plc.Contracts.Request;

public class GetS7NetPageRequest : PagingQuery
{
    public string? Ip { get; set; }

    public S7TypeEnum? S7Type { get; set; }
}