using Common.Application.MediatR.Message;
using Identity.Contrancts;

namespace Identity.Application.GetQuery;

public class GetRoleQuery : IQuery<IEnumerable<RoleDto>>
{
}