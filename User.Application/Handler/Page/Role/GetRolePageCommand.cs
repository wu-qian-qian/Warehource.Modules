using Common.Application.MediatR.Message.PageQuery;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Page.Role;

public class GetRolePageCommand : PageQuery<RoleDto>
{
    public string? RoleName { get; set; }
}