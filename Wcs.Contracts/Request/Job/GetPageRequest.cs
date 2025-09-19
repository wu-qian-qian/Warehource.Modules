using Common.Shared;

namespace Wcs.Contracts.Request.Job;

public class GetPageRequest : PagingQuery
{
    public string? Name { get; set; }

    public string? JobType { get; set; }
}