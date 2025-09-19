using Common.Shared;

namespace Wcs.Contracts.Request.Region;

public class GetRegionPageRequest : PagingQuery
{
    public string? Code { get; set; }
}