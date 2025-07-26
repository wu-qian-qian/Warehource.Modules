using Common.Application.MediatR.Message;
using Identity.Contrancts;

namespace Identity.Application.GetQuery.Role;

public class GetRoleQuery : IQuery<IEnumerable<RoleDto>>
{
}