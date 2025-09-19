using Common.Shared;

namespace Identity.Contrancts.Request.Page;

public class GetRolePageRequest : PagingQuery
{
    public string? RoleName { get; set; }
}