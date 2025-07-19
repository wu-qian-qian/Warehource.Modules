using Common.Application.MediatR.Message;
using User.Contrancts;

namespace User.Application.GetQuery;

internal class GetUserQuery : IQuery<IEnumerable<UserDto>>
{
}

internal class GetRoleQuery : IQuery<IEnumerable<RoleDto>>
{
}