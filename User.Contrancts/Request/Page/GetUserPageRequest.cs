using Common.Shared;

namespace Identity.Contrancts.Request.Page;

public class GetUserPageRequest : PagingQuery
{
    public string? Name { get; set; }
}